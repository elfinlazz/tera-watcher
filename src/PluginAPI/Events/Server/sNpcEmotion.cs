using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sNpcEmotionHandler(sNpcEmotionArgs args);

	public class sNpcEmotionArgs : System.EventArgs {
		public ulong target;
		public ulong creature;
		public int emotion;
	}
}
