namespace TeraWatcherAPI.Events {
	public delegate void sPartyUpdateHandler(sPartyUpdateArgs args);

	public class sPartyUpdateArgs : System.EventArgs {
		public ulong target;
		public int currentHp;
		public int currentMp;
		public int maxHp;
		public int maxMp;
		public short level;
		public short inCombat;
		public short vitality;
		public byte status;
		public int stamina;
		public int currentRe;
		public int maxRe;
		public int unk;
	}
}
