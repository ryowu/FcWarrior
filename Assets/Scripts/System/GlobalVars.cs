
public static class GlobalVars
{
	public static bool IsCameraFollowing;
	public static bool IsPlayerControllable;
	//ingore for now
	public static int CheckPointPosition;

	public static bool BossDefeat;

	//Test assist
	public static bool TrapSafe = false;
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


