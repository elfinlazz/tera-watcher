using System.Collections.Generic;
using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sConditionListHandler(sConditionListArgs args);

	public class sConditionListArgs : System.EventArgs {
		public ulong target;
		public List<Condition> conditions;
	}
}
