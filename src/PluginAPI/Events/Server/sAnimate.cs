namespace TeraWatcherAPI.Events {
	public delegate void sAnimateHandler(sAnimateArgs args);

	public class sAnimateArgs : System.EventArgs {
		public ulong target;
		public int animation;
		public int unk1;
		public byte unk2;
	}
}
