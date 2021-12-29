
public static class GlobalVars
{
	public static bool IsCameraFollowing;
	public static bool IsPlayerControllable;
	//after enter secret room, use checkpoint to reset player position when exit room
	public static bool UseCheckPoint = false;

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

	public static bool SideWeaponChargeReady = false;
	public static int SideWeaponCooldownPeriod = 1200;




	//Stage Skills
	public static bool RockGunEnabled = false;
	public static bool DoubleJumpEnabled = false;
	public static bool PowerSword = false;
	//public static bool RockGunEnabled = true;
	//public static bool DoubleJumpEnabled = true;
	//public static bool PowerSword = true;

	//Secret Skills
	public static bool DoubleGun = false;
	public static bool DoubleDef = false;
	public static bool RecoverPowerUp = false;
	public static bool SideWeaponCostHalf = false;
	public static bool SideWeaponCooldownHalf = false;

	//public static bool DoubleGun = true;
	//public static bool DoubleDef = true;
	//public static bool RecoverPowerUp = true;
	//public static bool SideWeaponCostHalf = true;
	//public static bool SideWeaponCooldownHalf = true;

	public static void ResetPlayer()
	{
		PlayerHP = 10;
		PlayerMaxHP = 10;
		PlayerDiamond = 0;
	}
}


