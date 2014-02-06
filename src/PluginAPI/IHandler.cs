using TeraWatcherAPI.Events;

namespace TeraWatcherAPI {
	public interface IHandler {
		event sPlayerInfoHandler sPlayerInfo;
		event sSelfStaminaHandler sSelfStamina;

		void Log(int level, string format, params object[] args);
	}
}
