namespace TeraWatcherAPI.Events {
	public delegate void sUpdateHpHandler(sUpdateHpArgs args);

	public class sUpdateHpArgs : System.EventArgs {
		public uint current;
		public uint max;
		public int diff;
		public uint type;
		public ulong target;
		public ulong source;
		public byte crit;
	}
}
