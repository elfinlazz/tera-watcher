using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sNpcUnloadHandler(sNpcUnloadArgs args);

	public class sNpcUnloadArgs : System.EventArgs {
		public ulong target;
		public Position position;
		public uint type;
		public int unk;
	}
}
