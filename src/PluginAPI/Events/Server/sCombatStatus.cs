namespace TeraWatcherAPI.Events {
	public delegate void sCombatStatusHandler(sCombatStatusArgs args);

	public class sCombatStatusArgs : System.EventArgs {
		public ulong target;
		public uint status;
	}
}
