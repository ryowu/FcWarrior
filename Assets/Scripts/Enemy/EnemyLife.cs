using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
	Animator anim;
	Rigidbody2D enemyBody;
	BoxCollider2D enemyCollider;
	bool isDieing = false;

	[SerializeField] private AudioSource dieSoundEffect;
	[SerializeField] private AudioSource hitSoundEffect;
	[SerializeField] private GameObject diamond;
	[SerializeField] private GameObject coin;

	public bool IsAlive;

	private EnemyData eDate;

	// Start is called before the first frame update
	void Start()
	{
		IsAlive = true;
		anim = GetComponent<Animator>();
		enemyBody = GetComponent<Rigidbody2D>();
		enemyCollider = GetComponent<BoxCollider2D>();
		eDate = GetComponent<EnemyData>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Bullet") && !isDieing)
		{
			eDate.EnemyHP -= 6;
			if (eDate.EnemyHP <= 0)
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
			Destroy(collision.gameObject);
		}
	}

	private void EnemyDie()
	{
		dieSoundEffect.Play();
		IsAlive = false;
		anim.SetTrigger("death");
	}

	private void DestroyEnemy()
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
		Destroy(this.gameObject);
	}
}
