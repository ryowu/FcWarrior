using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsController : MonoBehaviour
{
	[SerializeField] private Text txtRockgun;
	[SerializeField] private Text txtDoubleJump;
	[SerializeField] private Text txtPowerSword;
	void Start()
	{
		SetSkillTextState();
	}

	public void SetSkillTextState()
	{
		txtRockgun.enabled = PlayerData.RockGunEnabled;
		txtDoubleJump.enabled = PlayerData.DoubleJumpEnabled;
		txtPowerSword.enabled = PlayerData.PowerSword;
	}
}
