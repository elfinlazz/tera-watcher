using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sLockedOnHandler(sLockedOnArgs args);

	public class sLockedOnArgs : System.EventArgs {
		public ulong source;
		public uint skill;
	}
}
