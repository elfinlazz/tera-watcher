namespace TeraWatcherAPI.Events {
	public delegate void sPartyUpdateHpHandler(sPartyUpdateHpArgs args);

	public class sPartyUpdateHpArgs : System.EventArgs {
		public ulong target;
		public int current;
		public int max;
	}
}
