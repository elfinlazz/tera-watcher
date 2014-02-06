using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sProjectileHandler(sProjectileArgs args);

	public class sProjectileArgs : System.EventArgs {
		public ulong source;
		public uint model;
		public int unk;
		public ulong id;
		public uint skill;
		public Position startPosition;
		public short startAngle;
		public Position targetPosition;
		public short targetAngle;
	}
}
