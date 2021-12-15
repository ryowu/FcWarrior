using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowExampleDoubleJump : MonoBehaviour
{
	DateTime dtExampleStart;
	bool stopExample = false;
	[SerializeField] private AudioSource jumpEffect;
	[SerializeField] private GameObject player;
	Rigidbody2D rb;

	bool inFirstJump;
	// Start is called before the first frame update
	void Start()
	{
		//Enable double jump
		PlayerData.DoubleJumpEnabled = true;

		dtExampleStart = DateTime.Now;
		GlobalVars.IsPlayerControllable = false;
		rb = player.GetComponent<Rigidbody2D>();
		rb.bodyType = RigidbodyType2D.Dynamic;

		inFirstJump = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (stopExample) return;
		if (Input.GetButtonDown("Fire1"))
			stopExample = true;

		GlobalVars.IsPlayerControllable = false;
		TimeSpan ts = DateTime.Now - dtExampleStart;
		if (ts.TotalMilliseconds > 3000f && !inFirstJump)
		{
			//show example
			jumpEffect.Play();
			rb.velocity = new Vector2(0f, 16f);
			inFirstJump = true;
		}
		
		if (ts.TotalMilliseconds > 3500f)
		{
			jumpEffect.Play();
			rb.velocity = new Vector2(0f, 16f);
			dtExampleStart = DateTime.Now;
			inFirstJump = false;
		}
	}
}
