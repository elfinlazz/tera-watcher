namespace TeraWatcherAPI.Events {
	public delegate void sLootRollHandler(sLootRollArgs args);

	public class sLootRollArgs : System.EventArgs {
		public ulong target;
		public int roll;
	}
}
