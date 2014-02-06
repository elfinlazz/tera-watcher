using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sAbnormalAddHandler(sAbnormalAddArgs args);

	public class sAbnormalAddArgs : System.EventArgs {
		public ulong target;
		public ulong source;
		public Abnormal abnormal;
	}
}
