namespace TeraWatcherAPI.Events {
	public delegate void sSelfInfoHandler(sSelfInfoArgs args);

	public class sSelfInfoArgs : System.EventArgs {
		public uint model;
		public ulong cid;
		public ulong pid;
		public byte[] appearance;
		public short level;
		public string name;
		public byte[] details1;
		public byte[] details2;
	}
}
