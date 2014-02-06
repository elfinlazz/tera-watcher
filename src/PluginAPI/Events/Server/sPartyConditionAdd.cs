using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sPartyConditionAddHandler(sPartyConditionAddArgs args);

	public class sPartyConditionAddArgs : System.EventArgs {
		public ulong target;
		public Condition condition;
	}
}
