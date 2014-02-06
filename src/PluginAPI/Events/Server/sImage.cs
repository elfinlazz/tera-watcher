namespace TeraWatcherAPI.Events {
	public delegate void sImageHandler(sImageArgs args);

	public class sImageArgs : System.EventArgs {
		public string name;
		public byte[] data;
	}
}
