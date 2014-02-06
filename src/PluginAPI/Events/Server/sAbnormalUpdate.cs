using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sAbnormalUpdateHandler(sAbnormalUpdateArgs args);

	public class sAbnormalUpdateArgs : System.EventArgs {
		public ulong target;
		public Abnormal abnormal;
	}
}
