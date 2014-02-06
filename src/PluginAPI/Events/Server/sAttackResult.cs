namespace TeraWatcherAPI.Events {
	public delegate void sAttackResultHandler(sAttackResultArgs args);

	public class sAttackResultArgs : System.EventArgs {
		public ulong source;
		public ulong target;
		public uint model;
		public uint skill;
		public uint stage;
		public int unk1;
		public uint id;
		public int time;
		public uint damage;
		public ushort type1a;
		public ushort type1b;
		public ushort crit;
	}
}
