using System;
using System.Collections.Generic;
using System.Diagnostics;

using TeraWatcherAPI;
using TeraWatcherAPI.Events;

namespace Logger {
	class Plugin : IPlugin {
		private Stopwatch Timer;

		private HashSet<ushort> Filters;
		private bool ShowKnown;

		public Plugin() {
			Filters = new HashSet<ushort> {
				// Server Packets
				0xE04F, // sPlayerMove
				0x5C0D, // (alliance crud)

				// Client Packets
				0xB3F3, // cMove
			};
			ShowKnown = true;

			Timer = new Stopwatch();
			Timer.Start();
		}

		public void Load(IHandler Handler) {
			Handler.sPacket += OnPacket(Handler, true);
			Handler.cPacket += OnPacket(Handler, false);
		}

		private gPacketHandler OnPacket(IHandler Handler, bool fromServer) {
			return delegate(gPacketArgs packet) {
				var opcode = BitConverter.ToUInt16(packet.data, 2);
				if (!(ShowKnown || packet.unknown)) return;
				if (Filters.Contains(opcode)) return;
				Handler.Log(2, "{0} {1} | {2} {3:X4} ({4,4}) | {5}",
					(packet.unknown ? ' ' : '*'),
					Timer.ElapsedMilliseconds,
					(fromServer ? "<-" : "->"),
					opcode,
					BitConverter.ToUInt16(packet.data, 0),
					BitConverter.ToString(packet.data).Replace('-', ' ')
				);
			};
		}
	}
}
