namespace TeraWatcherAPI.Events {
	public delegate void sLootRemoveHandler(sLootRemoveArgs args);

	public class sLootRemoveArgs : System.EventArgs {
		public ulong id;
	}
}
