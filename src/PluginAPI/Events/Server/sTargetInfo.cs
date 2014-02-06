using System.Collections.Generic;
using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sTargetInfoHandler(sTargetInfoArgs args);

	public class sTargetInfoArgs : System.EventArgs {
		public ulong target;
		public float hp;
		public uint level;
		public Edge edge;
		public List<Abnormal> abnormals;
		public List<Condition> conditions;
	}
}
