using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowExamplePowerSword : MonoBehaviour
{
	DateTime dtExampleStart;
	bool stopExample = false;
	[SerializeField] private AudioSource swordEffect;
	[SerializeField] private GameObject swordObject;

	// Start is called before the first frame update
	void Start()
	{
		//Enable double jump
		PlayerData.PowerSword = true;

		dtExampleStart = DateTime.Now;
		GlobalVars.IsPlayerControllable = false;

	}

	// Update is called once per frame
	void Update()
	{
		if (stopExample) return;
		if (Input.GetButtonDown("Fire1"))
			stopExample = true;

		GlobalVars.IsPlayerControllable = false;
		TimeSpan ts = DateTime.Now - dtExampleStart;
		if (ts.TotalMilliseconds > 1000f)
		{
			//show example
			Vector3 pos = new Vector3(-6.5f, -2.13f, 0f);
			GameObject swordNew = Instantiate(swordObject, pos, transform.rotation, transform);
			SpriteRenderer sr = swordNew.GetComponent<SpriteRenderer>();
			swordNew.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			swordEffect.Play();

			dtExampleStart = DateTime.Now;
		}
	}
}
