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

	private SpriteRenderer rd;

	[SerializeField] private AudioSource dieSoundEffect;
	[SerializeField] private AudioSource hitSoundEffect;
	[SerializeField] private HealthyBarController healthyBar;

	// Start is called before the first frame update
	void Start()
	{
		if (SceneManager.GetActiveScene().name == "Stage1_1" || SceneManager.GetActiveScene().name == "Stage2_1" ||
			SceneManager.GetActiveScene().name == "Stage3_1" || SceneManager.GetActiveScene().name == "Stage_Newbie")
			PlayerData.ResetPlayer();

		hp = PlayerData.PlayerHP;
		healthyBar.SetHPMaxValue(PlayerData.PlayerMaxHP);
		healthyBar.SetHPValue(hp);
		anim = GetComponent<Animator>();
		playerBody = GetComponent<Rigidbody2D>();
		rd = GetComponent<SpriteRenderer>();
		hitStartTime = DateTime.Now;
	}

	private void Update()
	{
		if (isHit)
		{
			TimeSpan ts = DateTime.Now - hitStartTime;
			if (ts.TotalMilliseconds > 2000f)
			{
				//GlobalVars.IsPlayerControllable = true;
				rd.color = new Color(255f, 255f, 255f, 1f);
				isHit = false;
			}
		}
	}


	private void OnEnemyCollide(Collision2D collision)
	{
		TimeSpan ts = DateTime.Now - hitStartTime;
		if (ts.TotalMilliseconds > 2000f)
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

			//save to global
			PlayerData.PlayerHP = hp;

			//destroy enemy bullet
			if (collision.gameObject.CompareTag("EnemyBullet"))
			{
				Destroy(collision.gameObject);
			}

			//GlobalVars.IsPlayerControllable = false;
			isHit = true;
			hitStartTime = DateTime.Now;
			//Player Hit
			rd.color = new Color(255f, 255f, 255f, 0.5f);
			//anim.SetInteger("state", 5);
			//anim.SetTrigger("hit");


			float vx = playerBody.velocity.x * -1f;
			if (playerBody.velocity.y > 0)
				playerBody.velocity = new Vector2(vx, 0f);
			else
				playerBody.velocity = new Vector2(vx, playerBody.velocity.y);

		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
		{
			OnEnemyCollide(collision);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Trap"))
		{
			PlayerDie();
		}
		else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
		{
			OnEnemyCollide(collision);
		}
	}

	private void PlayerDie()
	{
		GlobalVars.IsPlayerControllable = false;

		PlayerData.ResetPlayer();

		dieSoundEffect.Play();
		anim.SetTrigger("death");
		playerBody.bodyType = RigidbodyType2D.Static;
	}

	private void RestartLevel()
	{
		if (SceneManager.GetActiveScene().buildIndex == 7)
		{
			GlobalVars.BossAbnormalSequenceEvent.StartAI = false;
		}

		anim.SetInteger("state", 0);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
