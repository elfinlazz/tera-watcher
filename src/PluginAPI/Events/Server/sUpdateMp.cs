namespace TeraWatcherAPI.Events {
	public delegate void sUpdateMpHandler(sUpdateMpArgs args);

	public class sUpdateMpArgs : System.EventArgs {
		public uint current;
		public uint max;
		public int diff;
		public uint type;
		public ulong target;
		public ulong source;
	}
}
