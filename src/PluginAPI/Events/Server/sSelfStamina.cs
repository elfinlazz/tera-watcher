namespace TeraWatcherAPI.Events {
	public delegate void sSelfStaminaHandler(sSelfStaminaArgs args);

	public class sSelfStaminaArgs : System.EventArgs {
		public int current;
		public int max;
		public short tier;
	}
}
