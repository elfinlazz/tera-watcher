namespace TeraWatcherAPI.Events {
	public delegate void sAbsorbDamageHandler(sAbsorbDamageArgs args);

	public class sAbsorbDamageArgs : System.EventArgs {
		public ulong target;
		public int amount;
	}
}
