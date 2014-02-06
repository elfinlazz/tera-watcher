using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sAbnormalRemoveHandler(sAbnormalRemoveArgs args);

	public class sAbnormalRemoveArgs : System.EventArgs {
		public ulong target;
		public uint id;
	}
}
