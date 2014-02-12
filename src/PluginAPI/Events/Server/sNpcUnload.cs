namespace TeraWatcherAPI.Events {
	public delegate void sNpcUnloadHandler(sNpcUnloadArgs args);

	public class sNpcUnloadArgs : System.EventArgs {
		public ulong target;
		public int unk1;
		public byte unk2;
	}
}
