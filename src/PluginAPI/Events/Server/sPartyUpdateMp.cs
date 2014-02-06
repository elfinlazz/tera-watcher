namespace TeraWatcherAPI.Events {
	public delegate void sPartyUpdateMpHandler(sPartyUpdateMpArgs args);

	public class sPartyUpdateMpArgs : System.EventArgs {
		public ulong target;
		public int current;
		public int max;
	}
}
