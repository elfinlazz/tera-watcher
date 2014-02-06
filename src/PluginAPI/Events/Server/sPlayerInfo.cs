using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sPlayerInfoHandler(sPlayerInfoArgs args);

	public class sPlayerInfoArgs : System.EventArgs {
		public ulong pid;
		public ulong uid;
		public Position position;
		public short angle;
		public int relation;
		public uint model;
		public string name;
		public string guild;
		public string guildRank;
		public string guildTitle;
		public string guildEmblem;
	}
}
