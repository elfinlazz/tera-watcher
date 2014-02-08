namespace TeraWatcherAPI.Events {
	public delegate void sWhisperHandler(sWhisperArgs args);

	public class sWhisperArgs : System.EventArgs {
		public ulong player;
		public byte unk1;
		public byte gm;
		public byte unk2;
		public string author;
		public string recipient;
		public string message;
	}
}
