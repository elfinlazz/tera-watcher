namespace TeraWatcherAPI.Events {
	public delegate void sConditionActivateHandler(sConditionActivateArgs args);

	public class sConditionActivateArgs : System.EventArgs {
		public uint id;
	}
}
