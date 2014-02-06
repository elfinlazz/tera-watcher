using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sPartyAbnormalAddHandler(sPartyAbnormalAddArgs args);

	public class sPartyAbnormalAddArgs : System.EventArgs {
		public ulong target;
		public ulong source;
		public Abnormal abnormal;
	}
}
