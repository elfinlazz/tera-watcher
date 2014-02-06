using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sProjectedAttackRemoveHandler(sProjectedAttackRemoveArgs args);

	public class sProjectedAttackRemoveArgs : System.EventArgs {
		public ulong id;
		public byte unk;
	}
}
