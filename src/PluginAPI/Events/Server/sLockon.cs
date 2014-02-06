using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sLockonHandler(sLockonArgs args);

	public class sLockonArgs : System.EventArgs {
		public ulong target;
		public uint attack;
		public byte result;
	}
}
