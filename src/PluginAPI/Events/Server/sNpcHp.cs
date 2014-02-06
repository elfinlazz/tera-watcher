using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void sNpcHpHandler(sNpcHpArgs args);

	public class sNpcHpArgs : System.EventArgs {
		public ulong target;
		public float hp;
		public byte unk1;
		public Edge edge;
	}
}
