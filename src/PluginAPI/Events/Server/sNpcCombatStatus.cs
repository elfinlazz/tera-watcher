namespace TeraWatcherAPI.Events {
	public delegate void sNpcCombatStatusHandler(sNpcCombatStatusArgs args);

	public class sNpcCombatStatusArgs : System.EventArgs {
		public ulong target;
		public byte status;
	}
}
