namespace TeraWatcherAPI.Events {
	public delegate void cTargetHandler(cTargetArgs args);

	public class cTargetArgs : System.EventArgs {
		public ulong target;
	}
}
