using TeraWatcherAPI.Types;

namespace TeraWatcherAPI.Events {
	public delegate void cMoveHandler(cMoveArgs args);

	public class cMoveArgs : System.EventArgs {
		public Position pos1;
		public short angle;
		public Position pos2;
	}
}
