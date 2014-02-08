using System.Collections.Generic;

namespace TeraWatcherAPI.Events {
	public delegate void sSystemMessageHandler(sSystemMessageArgs args);

	public class sSystemMessageArgs : System.EventArgs {
		public string id;
		public Dictionary<string, string> args;
	}
}
