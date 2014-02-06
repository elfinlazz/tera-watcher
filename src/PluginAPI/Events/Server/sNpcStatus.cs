namespace TeraWatcherAPI.Events {
	public delegate void sNpcStatusHandler(sNpcStatusArgs args);

	public class sNpcStatusArgs : System.EventArgs {
		public ulong creature;
		public byte enraged;
		public int unk1;
		public ulong target;
		public int unk2;
	}
}
