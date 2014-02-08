namespace TeraWatcherAPI.Events {
	public delegate void sLootWindowHandler(sLootWindowArgs args);

	public class sLootWindowArgs : System.EventArgs {
		public uint item;
		public int duration;
	}
}
