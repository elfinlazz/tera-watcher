namespace TeraWatcherAPI.Events {
	public delegate void sUpdateStatsHandler(sUpdateStatsArgs args);

	public class sUpdateStatsArgs : System.EventArgs {
		public int currentHp;
		public int currentMp;
		public int unk1;
		public int maxHp;
		public int maxMp;

		// base
		public int basePower;
		public int baseEndurance;
		public int baseImpactFactor;
		public int baseBalanceFactor;
		public short baseMovementSpeed;
		public short baseUnkSpeed;
		public short baseAttackSpeed;
		public float baseCritRate;
		public float baseCritResist;
		public float baseCritPower;
		public int baseAttack;
		public int baseAttack2;
		public int baseDefense;
		public int baseImpact;
		public int baseBalance;
		public float baseResistWeakening;
		public float baseResistPeriodic;
		public float baseResistStun;

		// bonus
		public int bonusPower;
		public int bonusEndurance;
		public int bonusImpactFactor;
		public int bonusBalanceFactor;
		public short bonusMovementSpeed;
		public short bonusUnkSpeed;
		public short bonusAttackSpeed;
		public float bonusCritRate;
		public float bonusCritResist;
		public float bonusCritPower;
		public int bonusAttack;
		public int bonusAttack2;
		public int bonusDefense;
		public int bonusImpact;
		public int bonusBalance;
		public float bonusResistWeakening;
		public float bonusResistPeriodic;
		public float bonusResistStun;

		public int level;
		public short vitality;
		public byte status;
		public int bonusHp;
		public int bonusMp;
		public int currentStamina;
		public int maxStamina;
		public int unk2;
		public int unk3;
		public int unk4;
		public int unk5;
		public int itemLevelInventory;
		public int itemLevel;
		public int unk6;
		public int unk7;
		public int unk8;
		public int unk9;
	}
}
