using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sConditionAddHandler(sConditionAddArgs args);

	public class sConditionAddArgs : System.EventArgs {
		public ulong target;
		public Condition condition;
	}
}
