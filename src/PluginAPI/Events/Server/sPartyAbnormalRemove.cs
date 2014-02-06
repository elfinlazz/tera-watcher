using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sPartyAbnormalRemoveHandler(sPartyAbnormalRemoveArgs args);

	public class sPartyAbnormalRemoveArgs : System.EventArgs {
		public ulong target;
		public uint id;
	}
}
