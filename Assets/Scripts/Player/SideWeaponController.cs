using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideWeaponController : MonoBehaviour
{
	private Image imgSideWeapon;
	private DateTime dtStart;

	void Start()
	{
		imgSideWeapon = GetComponent<Image>();
		imgSideWeapon.color = new Color(255f, 255f, 255f, 0.5f);
		PlayerData.SideWeaponChargeReady = false;
		dtStart = DateTime.Now;
	}

	// Update is called once per frame
	void Update()
	{
		if (PlayerData.SideWeaponChargeReady) return;

		TimeSpan ts = DateTime.Now - dtStart;
		if (ts.TotalMilliseconds >= PlayerData.SideWeaponCooldownPeriod)
		{
			dtStart = DateTime.Now;
			PlayerData.SideWeaponChargeReady = true;
			imgSideWeapon.color = new Color(255f, 255f, 255f, 1f);
		}
		else
		{
			PlayerData.SideWeaponChargeReady = false;
			imgSideWeapon.color = new Color(255f, 255f, 255f, 0.5f);
		}
	}
}
