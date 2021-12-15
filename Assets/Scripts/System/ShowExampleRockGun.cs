using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowExampleRockGun : MonoBehaviour
{
	[SerializeField] private GameObject PlayerBullet;
	[SerializeField] private AudioSource shootEffect;

	[SerializeField] private GameObject rockExample;

	DateTime dtExampleStart;
	bool stopExample = false;

	// Start is called before the first frame update
	void Start()
	{
		//Enable Rock gun
		PlayerData.RockGunEnabled = true;

		dtExampleStart = DateTime.Now;
		GlobalVars.IsPlayerControllable = false;

		//init rock
		Vector3 newRockPos = new Vector3(transform.position.x + 15f, transform.position.y - 0.1f, transform.position.z);
		GameObject rockNew = Instantiate(rockExample, newRockPos, transform.rotation, transform.parent);
	}

	// Update is called once per frame
	void Update()
	{
		if (stopExample) return;
		if (Input.GetButtonDown("Fire1"))
			stopExample = true;
		
		GlobalVars.IsPlayerControllable = false;
		TimeSpan ts = DateTime.Now - dtExampleStart;
		if (ts.TotalMilliseconds > 3000f)
		{
			//show example

			//init bullet
			GameObject bulletNew = Instantiate(PlayerBullet, transform.position, transform.rotation, transform.parent);
			FireballMoving fb = bulletNew.GetComponent<FireballMoving>();
			fb.TargetPostion = new Vector3(transform.position.x + 20f, transform.position.y, transform.position.z);
			fb.MovingSpeed = 35f;
			shootEffect.Play();

			//init rock
			Vector3 newRockPos = new Vector3(transform.position.x + 15f, transform.position.y - 0.1f, transform.position.z);
			GameObject rockNew = Instantiate(rockExample, newRockPos, transform.rotation, transform.parent);

			dtExampleStart = DateTime.Now;
		}
	}
}
