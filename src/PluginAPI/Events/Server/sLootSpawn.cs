using System.Collections.Generic;
using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sLootSpawnHandler(sLootSpawnArgs args);

	public class sLootSpawnArgs : System.EventArgs {
		public ulong id;
		public Position position;
		public uint item;
		public uint amount;
		public int expiry;
		public ulong mob;
		public List<ulong> owners;
	}
}
