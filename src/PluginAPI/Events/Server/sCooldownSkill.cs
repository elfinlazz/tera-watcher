namespace TeraWatcherAPI.Events {
	public delegate void sCooldownSkillHandler(sCooldownSkillArgs args);

	public class sCooldownSkillArgs : System.EventArgs {
		public uint skill;
		public int cooldown;
	}
}
