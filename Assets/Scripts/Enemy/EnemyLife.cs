using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
	protected Animator anim;
	Rigidbody2D enemyBody;
	protected BoxCollider2D enemyCollider;
	protected bool isDieing = false;

	[SerializeField] private AudioSource dieSoundEffect;
	[SerializeField] protected AudioSource hitSoundEffect;
	[SerializeField] private GameObject diamond;
	[SerializeField] private GameObject coin;
	[SerializeField] private bool DropItems = true;

	public bool IsAlive;

	public EnemyData eData;

	public bool IsImmune;

	// Start is called before the first frame update
	void Start()
	{
		IsAlive = true;
		IsImmune = false;
		anim = GetComponent<Animator>();
		enemyBody = GetComponent<Rigidbody2D>();
		enemyCollider = GetComponent<BoxCollider2D>();
		eData = GetComponent<EnemyData>();
		InnerStart();
	}

	protected virtual void InnerStart()
	{ }

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (IsImmune) return;

		if ((collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("PlayerSword")) && !isDieing)
		{
			eData.EnemyHP -= 6;

			RefreshHPBar();

			if (eData.EnemyHP <= 0)
			{
				isDieing = true;
				enemyCollider.isTrigger = true;

				EnemyDie();
			}
			else
			{
				hitSoundEffect.Play();
			}

			//Destroy bullet
			if (collision.gameObject.CompareTag("Bullet"))
				Destroy(collision.gameObject);
		}
	}

	protected virtual void RefreshHPBar() { }

	protected virtual void OnEnemyDie() { }

	protected void EnemyDie()
	{
		dieSoundEffect.Play();
		IsAlive = false;
		anim.SetTrigger("death");
	}

	private void DestroyEnemy()
	{
		if (DropItems)
		{
			float rndNum = Random.Range(-10.0f, 10.0f);
			if (rndNum >= 5f)
			{
				GameObject diamondNew = Instantiate(diamond, transform.position, transform.rotation, transform.parent);
			}
			else
			{
				GameObject coinNew = Instantiate(coin, transform.position, transform.rotation, transform.parent);
			}
		}
		OnEnemyDie();
		Destroy(this.gameObject);
	}
}
