namespace TeraWatcherAPI.Events {
	public delegate void sPartyInvitePrivHandler(sPartyInvitePrivArgs args);

	public class sPartyInvitePrivArgs : System.EventArgs {
		public ulong target;
		public byte canInvite;
	}
}
