using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sProjectedAttackHandler(sProjectedAttackArgs args);

	public class sProjectedAttackArgs : System.EventArgs {
		public ulong id;
		public Position startPosition;
		public short startAngle;
		public Position targetPosition;
		public short targetAngle;
		public ulong source;
		public uint model;
	}
}
