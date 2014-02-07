namespace TeraWatcherAPI.Events {
	public delegate void sLootWonHandler(sLootWonArgs args);

	public class sLootWonArgs : System.EventArgs {
		public ulong target;
	}
}
