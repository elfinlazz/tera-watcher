using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sPartyAbnormalUpdateHandler(sPartyAbnormalUpdateArgs args);

	public class sPartyAbnormalUpdateArgs : System.EventArgs {
		public ulong target;
		public Abnormal abnormal;
	}
}
