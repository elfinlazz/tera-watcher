namespace TeraWatcherAPI.Events {
	public delegate void sLootQueueHandler(sLootQueueArgs args);

	public class sLootQueueArgs : System.EventArgs {
		public int count;
	}
}
