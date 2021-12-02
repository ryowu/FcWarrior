using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
	private int hp;

	Animator anim;
	Rigidbody2D playerBody;

	bool isHit = false;
	DateTime hitStartTime;

	[SerializeField] private AudioSource dieSoundEffect;
	[SerializeField] private AudioSource hitSoundEffect;


	[SerializeField] private HealthyBarController healthyBar;

	// Start is called before the first frame update
	void Start()
	{
		hp = 20;
		healthyBar.SetHPMaxValue(hp);
		healthyBar.SetHPValue(hp);
		anim = GetComponent<Animator>();
		playerBody = GetComponent<Rigidbody2D>();
		hitStartTime = DateTime.Now;
	}

	private void Update()
	{
		if (isHit)
		{
			TimeSpan ts = DateTime.Now - hitStartTime;
			if (ts.TotalMilliseconds > 300f)
			{
				GlobalVars.IsPlayerControllable = true;

			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Trap"))
		{
			PlayerDie();
		}
		else if(collision.gameObject.CompareTag("Enemy"))
		{
			TimeSpan ts = DateTime.Now - hitStartTime;
			if (ts.TotalMilliseconds > 300f)
			{
				hitSoundEffect.Play();

				//refresh hp bar
				EnemyData eData = collision.gameObject.GetComponent<EnemyData>();
				hp -= eData.EnemyATK;
				healthyBar.SetHPValue(hp);
				if (hp <= 0)
				{
					PlayerDie();
					return;
				}
				
				GlobalVars.IsPlayerControllable = false;
				isHit = true;
				hitStartTime = DateTime.Now;
				//Player Hit
				anim.SetInteger("state", 5);

				anim.SetTrigger("hit");

				
				float vx = playerBody.velocity.x * -1f;
				if (playerBody.velocity.y > 0)
					playerBody.velocity = new Vector2(vx, 0f);
				else
					playerBody.velocity = new Vector2(vx, playerBody.velocity.y);

			}

			//PlayerDie();
		}
	}

	private void PlayerDie()
	{
		dieSoundEffect.Play();
		anim.SetTrigger("death");
		playerBody.bodyType = RigidbodyType2D.Static;
	}

	private void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
