namespace TeraWatcherAPI.Events {
	public delegate void cLockonHandler(cLockonArgs args);

	public class cLockonArgs : System.EventArgs {
		public ulong target;
		public uint attack;
	}
}
