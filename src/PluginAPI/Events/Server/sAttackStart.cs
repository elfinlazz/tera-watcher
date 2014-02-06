using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sAttackStartHandler(sAttackStartArgs args);

	public class sAttackStartArgs : System.EventArgs {
		public ulong source;
		public Position position;
		public short angle;
		public uint model;
		public uint skill;
		public uint stage;
		public float speed;
		public uint id;
	}
}
