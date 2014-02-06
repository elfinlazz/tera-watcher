namespace TeraWatcherAPI.Events {
	public delegate void sPartyUpdateReHandler(sPartyUpdateReArgs args);

	public class sPartyUpdateReArgs : System.EventArgs {
		public ulong target;
		public int current;
		public int max;
		public int unk;
	}
}
