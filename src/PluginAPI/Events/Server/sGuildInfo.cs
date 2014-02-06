using System.Collections.Generic;
using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sGuildInfoHandler(sGuildInfoArgs args);

	public class sGuildInfoArgs : System.EventArgs {
		public string name;
		public string title;
		public string master;
		public string motd;
		public string unk;
		public string ad;
		public List<GuildRank> ranks;
	}
}
