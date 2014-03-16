using System.Collections.Generic;
using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sAttackHomingHandler(sAttackHomingArgs args);

	public class sAttackHomingArgs : System.EventArgs {
		public ulong source;
		public uint model;
		public uint skill;
		public uint id;
		public List<ulong> targets;
		public List<Position> positions;
	}
}
