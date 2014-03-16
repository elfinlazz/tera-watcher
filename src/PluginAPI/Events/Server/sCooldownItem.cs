namespace TeraWatcherAPI.Events {
	public delegate void sCooldownItemHandler(sCooldownItemArgs args);

	public class sCooldownItemArgs : System.EventArgs {
		public uint item;
		public int cooldown;
	}
}
