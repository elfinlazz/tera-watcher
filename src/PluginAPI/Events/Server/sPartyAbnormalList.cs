using System.Collections.Generic;
using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sPartyAbnormalListHandler(sPartyAbnormalListArgs args);

	public class sPartyAbnormalListArgs : System.EventArgs {
		public ulong target;
		public List<Abnormal> abnormals;
		public List<Condition> conditions;
	}
}
