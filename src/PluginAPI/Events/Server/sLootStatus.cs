namespace TeraWatcherAPI.Events {
	public delegate void sLootStatusHandler(sLootStatusArgs args);

	public class sLootStatusArgs : System.EventArgs {
		public ulong id;
		public byte waiting;
	}
}
