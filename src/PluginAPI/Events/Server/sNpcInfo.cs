using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sNpcInfoHandler(sNpcInfoArgs args);

	public class sNpcInfoArgs : System.EventArgs {
		public ulong id;
		public ulong target;
		public Position position;
		public short angle;
		public uint npc;
		public uint type;
		public uint model;
		public ulong owner;
	}
}
