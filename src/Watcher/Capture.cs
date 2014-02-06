using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

using Crypt;
using SharpPcap;

namespace TeraPacketEncryption {
	class Capture {
		protected Parse Parse;
		protected Session Session;
		protected IPAddress ServerAddress;
		protected int State;
		protected uint ServerSeqNum;
		protected uint ClientSeqNum;
		protected byte[] ServerBuffer;
		protected byte[] ClientBuffer;
		protected PriorityQueue<PacketDotNet.TcpPacket> TcpBuffer;

		private bool Terminate = false;
		private object QueueLock = new object();
		private List<RawCapture> PacketQueue = new List<RawCapture>();

		public Capture(int deviceIndex, string serverAddress, Parse parse) {
			ServerAddress = IPAddress.Parse(serverAddress);
			Parse = parse;

			var device = CaptureDeviceList.Instance[deviceIndex];
			device.Open(DeviceMode.Normal, 16);
			device.Filter = String.Format("host {0} and tcp", ServerAddress);

			Console.WriteLine("<watching on {0}>", ServerAddress);

			Reset();
			var thread = new Thread(ProcessThread);
			thread.Start();

			device.OnPacketArrival += new PacketArrivalEventHandler(device_PcapOnPacketArrival);

			device.Capture();
			device.Close(); // we shouldn't get here but just in case
			thread.Join(); // this can't even happen
		}

		private void Reset() {
			State = -1;
			Session = new Session();
			ServerBuffer = new byte[0];
			ClientBuffer = new byte[0];
			ServerSeqNum = 0;
			ClientSeqNum = 0;
			TcpBuffer = new PriorityQueue<PacketDotNet.TcpPacket>();
		}

		public void device_PcapOnPacketArrival(object sender, CaptureEventArgs e) {
			lock (QueueLock) {
				PacketQueue.Add(e.Packet);
			}
		}

		private void ProcessThread() {
			while (true) {
				bool naptime = true;
				lock (QueueLock) {
					if (PacketQueue.Count != 0) {
						naptime = false;
					}
				}

				if (naptime) {
					Thread.Sleep(66); // 15 fps
					continue;
				}

				List<RawCapture> queue;
				lock (QueueLock) {
					queue = PacketQueue;
					PacketQueue = new List<RawCapture>();
				}

				foreach (var packet in queue) {
					ProcessPacket(packet);
				}
			}
		}

		private void ProcessPacket(RawCapture Packet) {
			var tcpPacket =
				PacketDotNet.Packet.ParsePacket(Packet.LinkLayerType, Packet.Data)
					.Extract(typeof(PacketDotNet.TcpPacket)) as PacketDotNet.TcpPacket;

			if (tcpPacket != null) {
				var fromServer = (tcpPacket.SourcePort == 10001); // hacky
				var data = tcpPacket.PayloadData;
				var length = data.Length;

				// handle TCP SYN (connection started)
				if (tcpPacket.Syn) {
					if (fromServer) {
						Console.WriteLine("<connection started>");
						Reset();
						ServerSeqNum = tcpPacket.SequenceNumber + 1;
					} else {
						ClientSeqNum = tcpPacket.SequenceNumber + 1;
					}
					return;
				}
				
				// handle TCP FIN (connection terminated)
				if (tcpPacket.Fin && State != -1) {
					Console.WriteLine("<connection terminated>");
					Reset();
					return;
				}

				// early exit if no data
				if (length == 0) return;

				// initial packet (state: -1)
				if (State == -1) {
					// check for "01 00 00 00"
					if (length == 4 && BitConverter.ToUInt32(data, 0) == 0x01) {
						ServerSeqNum = tcpPacket.SequenceNumber + 4;
						State = 0;
					}
					return;
				}

				if (fromServer) {
					if (tcpPacket.SequenceNumber > ServerSeqNum) {
						Console.WriteLine("out-of-order packet {0} (expected {1}) :: queue -> {2}", tcpPacket.SequenceNumber, ServerSeqNum, TcpBuffer.Count + 1);
						TcpBuffer.Enqueue(tcpPacket, tcpPacket.SequenceNumber);
						return;
					}

					while (tcpPacket != null && tcpPacket.SequenceNumber <= ServerSeqNum) {
						data = tcpPacket.PayloadData;
						length = data.Length;

						// check for old seq
						int rewind = (int)(ServerSeqNum - tcpPacket.SequenceNumber);
						if (rewind > 0) { // seq in past?
							if (length - rewind <= 0) { // no additional data?
								Console.Write("duplicate packet {0}, dropping :: queue -> {1}", tcpPacket.SequenceNumber, TcpBuffer.Count);
								if (TcpBuffer.Count > 0) Console.Write(", next = {0}", TcpBuffer.Peek().SequenceNumber);
								Console.WriteLine();

								tcpPacket = (TcpBuffer.Count > 0) ? TcpBuffer.Dequeue() : null;
								continue;
							}

							// catch up
							length -= rewind;
							Console.Write("catching up, +{0} bytes :: queue -> {1}", length, TcpBuffer.Count);
							if (TcpBuffer.Count > 0) Console.Write(", next = {0}", TcpBuffer.Peek().SequenceNumber);
							Console.WriteLine();

							Array.Copy(data, rewind, data, 0, length);
							Array.Resize(ref data, length);
						}

						// advance expected sequence number
						ServerSeqNum += (uint)length;

						// add to server buffer
						if (State == 2) Session.Encrypt(ref data);
						AppendBuffer(ref ServerBuffer, data);

						// get next packet in queue
						tcpPacket = (TcpBuffer.Count > 0) ? TcpBuffer.Dequeue() : null;
						if (tcpPacket != null) {
							Console.Write("fetching buffered packet :: queue -> {0}", TcpBuffer.Count);
							if (TcpBuffer.Count > 0) Console.Write(", next = {0}", TcpBuffer.Peek().SequenceNumber);
							Console.WriteLine();
						}
					}

					while (ProcessServerData());
				} else {
					if (tcpPacket.SequenceNumber < ClientSeqNum) {
						uint rewind = ClientSeqNum - tcpPacket.SequenceNumber;
						if (length <= rewind) return;

						length -= (int)rewind;
						Array.Copy(data, rewind, data, 0, length);
						Array.Resize(ref data, length);
					}

					if (State == 2) Session.Decrypt(ref data);
					AppendBuffer(ref ClientBuffer, data);
					while (ProcessClientData());
				}
			}
		}

		protected void AppendBuffer(ref byte[] target, byte[] source) {
			Array.Resize(ref target, target.Length + source.Length);
			Array.Copy(source, 0, target, target.Length - source.Length, source.Length);
		}

		protected byte[] GetData(ref byte[] buffer, int length) {
			byte[] result = new byte[length];
			Array.Copy(buffer, result, length);

			byte[] reserve = (byte[])buffer.Clone();
			buffer = new byte[buffer.Length - length];
			Array.Copy(reserve, length, buffer, 0, buffer.Length);

			return result;
		}

		protected bool ProcessServerData() {
			switch (State) {
				case 0:
					if (ServerBuffer.Length < 128) return false;
					Session.ServerKey1 = GetData(ref ServerBuffer, 128);
					State++;
					return true;
				case 1:
					if (ServerBuffer.Length < 128) return false;
					Session.ServerKey2 = GetData(ref ServerBuffer, 128);
					Session.Init();
					State++;
					return true;
			}

			if (ServerBuffer.Length < 4) return false;

			int length = BitConverter.ToUInt16(ServerBuffer, 0);
			if (ServerBuffer.Length < length) return false;

			ushort opCode = BitConverter.ToUInt16(ServerBuffer, 2);
			byte[] data = GetData(ref ServerBuffer, length);

			Parse.ServerCommand(opCode, data);

			return true;
		}

		protected bool ProcessClientData() {
			switch (State) {
				case 0:
					if (ClientBuffer.Length < 128) return false;
					Session.ClientKey1 = GetData(ref ClientBuffer, 128);
					return true;
				case 1:
					if (ClientBuffer.Length < 128) return false;
					Session.ClientKey2 = GetData(ref ClientBuffer, 128);
					return true;
			}

			if (ClientBuffer.Length < 4) return false;

			int length = BitConverter.ToUInt16(ClientBuffer, 0);
			if (ClientBuffer.Length < length) return false;

			ushort opCode = BitConverter.ToUInt16(ClientBuffer, 2);
			byte[] data = GetData(ref ClientBuffer, length);

			Parse.ClientCommand(opCode, data);
			
			return true;
		}
	}
}
