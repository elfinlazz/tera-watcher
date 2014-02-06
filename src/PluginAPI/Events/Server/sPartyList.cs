using System.Collections.Generic;
using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sPartyListHandler(sPartyListArgs args);

	public class sPartyListArgs : System.EventArgs {
		public byte type;
		public ulong leader;
		public List<PartyMember> members;
	}
}
