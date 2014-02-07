namespace TeraWatcherAPI.Events {
	public delegate void sLootReceivedHandler(sLootReceivedArgs args);

	public class sLootReceivedArgs : System.EventArgs {
		public ulong target;
		public int unk1;
		public int unk2;
		public ulong id;
		public byte unk3;
	}
}
