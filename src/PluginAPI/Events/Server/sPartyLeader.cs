namespace TeraWatcherAPI.Events {
	public delegate void sPartyLeaderHandler(sPartyLeaderArgs args);

	public class sPartyLeaderArgs : System.EventArgs {
		public ulong target;
		public string name;
	}
}
