using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sProjectileRemoveHandler(sProjectileRemoveArgs args);

	public class sProjectileRemoveArgs : System.EventArgs {
		public ulong id;
		public byte unk;
	}
}
