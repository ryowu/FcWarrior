
public static class GlobalVars
{
	public static bool IsCameraFollowing;
	public static bool IsPlayerControllable;

	public static int CheckPointPosition;

	//Test assist
	public static bool TrapSafe = false;


	public static class BossAbnormalSequenceEvent
	{
		public static bool DisableOriginalBGM = false;
		public static bool PlayWarning = false;
		public static bool BossShowUp = false;
		public static bool BossHPShowUp = false;
		public static bool PlayBossBGM = false;
		public static bool StartAI = false;
		public static bool StopBossBGM = false;
	}

}


public static class PlayerData
{
	


	public static int PlayerLife = 3;
	public static int PlayerHP = 10;
	public static int PlayerMaxHP = 10;
	public static int PlayerDiamond = 0;


	public static bool RockGunEnabled = false;
	public static bool DoubleJumpEnabled = false;
	public static bool PowerSword = false;

	//public static bool RockGunEnabled = true;
	//public static bool DoubleJumpEnabled = true;
	//public static bool PowerSword = true;

	public static void ResetPlayer()
	{
		PlayerHP = 10;
		PlayerMaxHP = 10;
		PlayerDiamond = 0;
	}
}


