namespace TeraWatcherAPI.Events {
	public delegate void sConditionRemoveHandler(sConditionRemoveArgs args);

	public class sConditionRemoveArgs : System.EventArgs {
		public uint id;
	}
}
