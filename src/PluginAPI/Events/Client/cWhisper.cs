namespace TeraWatcherAPI.Events {
	public delegate void cWhisperHandler(cWhisperArgs args);

	public class cWhisperArgs : System.EventArgs {
		public string target;
		public string message;
	}
}
