using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sLifeStatusHandler(sLifeStatusArgs args);

	public class sLifeStatusArgs : System.EventArgs {
		public ulong target;
		public Position position;
		public ushort status;
		public byte unk;
	}
}
