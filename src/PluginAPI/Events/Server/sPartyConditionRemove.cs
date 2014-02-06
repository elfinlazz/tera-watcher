namespace TeraWatcherAPI.Events {
	public delegate void sPartyConditionRemoveHandler(sPartyConditionRemoveArgs args);

	public class sPartyConditionRemoveArgs : System.EventArgs {
		public ulong target;
		public uint id;
	}
}
