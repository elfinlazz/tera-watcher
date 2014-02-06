namespace TeraWatcherAPI.Events {
	public delegate void sPartyDeathHandler(sPartyDeathArgs args);

	public class sPartyDeathArgs : System.EventArgs {
		public ulong target;
	}
}
