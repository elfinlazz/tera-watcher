namespace TeraWatcherAPI.Events {
	public delegate void sChatMessageHandler(sChatMessageArgs args);

	public class sChatMessageArgs : System.EventArgs {
		public uint type;
		public ulong authorId;
		public byte unk1;
		public byte gm;
		public byte unk2;
		public string authorName;
		public string message;
	}
}
