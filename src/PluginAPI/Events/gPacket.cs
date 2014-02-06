namespace TeraWatcherAPI.Events {
	public delegate void gPacketHandler(gPacketArgs args);

	public class gPacketArgs : System.EventArgs {
		public byte[] data;
		public bool unknown;
	}
}
