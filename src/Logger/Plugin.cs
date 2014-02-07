using System;
using System.Diagnostics;

using TeraWatcherAPI;
using TeraWatcherAPI.Events;

namespace Logger {
	class Plugin : IPlugin {
		private Stopwatch Timer;

		public Plugin() {
			Timer = new Stopwatch();
			Timer.Start();
		}

		public void Load(IHandler Handler) {
			Handler.sPacket += OnPacket(Handler, true);
			Handler.cPacket += OnPacket(Handler, false);
		}

		private gPacketHandler OnPacket(IHandler Handler, bool fromServer) {
			return delegate(gPacketArgs packet) {
				Handler.Log(2, "{0} {1} | {2} {3:X4} ({4,4}) | {5}",
					(packet.unknown ? ' ' : '*'),
					Timer.ElapsedMilliseconds,
					(fromServer ? "<-" : "->"),
					BitConverter.ToUInt16(packet.data, 2),
					BitConverter.ToUInt16(packet.data, 0),
					BitConverter.ToString(packet.data).Replace('-', ' ')
				);
			};
		}
	}
}
