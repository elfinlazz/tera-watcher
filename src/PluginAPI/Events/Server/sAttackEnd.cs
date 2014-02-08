using System.Collections.Generic;
using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sAttackEndHandler(sAttackEndArgs args);

	public class sAttackEndArgs : System.EventArgs {
		public ulong source;
		public uint model;
		public uint skill;
		public uint id;
		public List<ulong> targets;
		public List<Position> positions;
	}
}
