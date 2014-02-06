namespace TeraWatcherAPI.Events {
	public delegate void sUpdateReHandler(sUpdateReArgs args);

	public class sUpdateReArgs : System.EventArgs {
		public uint current;
		public uint max;
		public int unk1;
		public int unk2;
		public int unk3;
	}
}
