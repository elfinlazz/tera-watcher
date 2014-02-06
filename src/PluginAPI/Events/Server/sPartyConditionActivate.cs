namespace TeraWatcherAPI.Events {
	public delegate void sPartyConditionActivateHandler(sPartyConditionActivateArgs args);

	public class sPartyConditionActivateArgs : System.EventArgs {
		public ulong target;
		public uint id;
	}
}
