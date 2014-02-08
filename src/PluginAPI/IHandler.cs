using TeraWatcherAPI.Events;

namespace TeraWatcherAPI {
	public interface IHandler {
		/*****************
		 * Server Events *
		 *****************/
		event gPacketHandler sPacket;

		event sAbnormalAddHandler sAbnormalAdd;
		event sAbnormalRemoveHandler sAbnormalRemove;
		event sAbnormalUpdateHandler sAbnormalUpdate;
		event sAbsorbDamageHandler sAbsorbDamage;
		event sAttackResultHandler sAttackResult;
		event sAttackStartHandler sAttackStart;
		event sChatMessageHandler sChatMessage;
		event sCombatStatusHandler sCombatStatus;
		event sConditionActivateHandler sConditionActivate;
		event sConditionAddHandler sConditionAdd;
		event sConditionListHandler sConditionList;
		event sConditionRemoveHandler sConditionRemove;
		event sGuildInfoHandler sGuildInfo;
		event sImageHandler sImage;
		event sLifeStatusHandler sLifeStatus;
		event sLockonHandler sLockon;
		event sLootReceivedHandler sLootReceived;
		event sLootRemoveHandler sLootRemove;
		event sLootRollHandler sLootRoll;
		event sLootSpawnHandler sLootSpawn;
		event sLootStatusHandler sLootStatus;
		event sLootWindowHandler sLootWindow;
		event sLootWonHandler sLootWon;
		event sNpcCombatStatusHandler sNpcCombatStatus;
		event sNpcEmotionHandler sNpcEmotion;
		event sNpcHpHandler sNpcHp;
		event sNpcStatusHandler sNpcStatus;
		event sPartyAbnormalAddHandler sPartyAbnormalAdd;
		event sPartyAbnormalListHandler sPartyAbnormalList;
		event sPartyAbnormalRemoveHandler sPartyAbnormalRemove;
		event sPartyAbnormalUpdateHandler sPartyAbnormalUpdate;
		event sPartyConditionActivateHandler sPartyConditionActivate;
		event sPartyConditionAddHandler sPartyConditionAdd;
		event sPartyConditionRemoveHandler sPartyConditionRemove;
		event sPartyDeathHandler sPartyDeath;
		event sPartyInvitePrivHandler sPartyInvitePriv;
		event sPartyLeaderHandler sPartyLeader;
		event sPartyListHandler sPartyList;
		event sPartyUpdateHandler sPartyUpdate;
		event sPartyUpdateHpHandler sPartyUpdateHp;
		event sPartyUpdateMpHandler sPartyUpdateMp;
		event sPartyUpdateReHandler sPartyUpdateRe;
		event sPlayerInfoHandler sPlayerInfo;
		event sProjectedAttackHandler sProjectedAttack;
		event sProjectedAttackRemoveHandler sProjectedAttackRemove;
		event sProjectileHandler sProjectile;
		event sProjectileRemoveHandler sProjectileRemove;
		event sSelfStaminaHandler sSelfStamina;
		event sTargetInfoHandler sTargetInfo;
		event sUpdateHpHandler sUpdateHp;
		event sUpdateMpHandler sUpdateMp;
		event sUpdateReHandler sUpdateRe;

		/*****************
		 * Client Events *
		 *****************/
		event gPacketHandler cPacket;

		event cLockonHandler cLockon;
		event cMoveHandler cMove;
		event cTargetHandler cTarget;
		event cWhisperHandler cWhisper;

		/***********
		 * Methods *
		 ***********/
		void Log(int level, string format, params object[] args);
	}
}
