namespace TeraWatcherAPI.Events {
	public delegate void sPlayerInfoHandler(sPlayerInfoArgs args);

	public class sPlayerInfoArgs : System.EventArgs {
		public ulong uid;
		public ulong pid;
	}
}
