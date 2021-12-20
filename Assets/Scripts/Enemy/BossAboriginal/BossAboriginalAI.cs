using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAboriginalAI : MonoBehaviour
{
	private EnemyBossLife bosslife;
	[SerializeField] private AudioSource bossBGM;
	[SerializeField] public GameObject Player;
	[SerializeField] private GameObject fireballHorizon;
	[SerializeField] private GameObject fireballRound;
	[SerializeField] private AudioSource fireballHorizonEffect;

	private SpriteRenderer sr;
	private Animator aim;

	DateTime hpRestoreEachFrameStartTime;

	float hp;

	// Start is called before the first frame update
	void Start()
	{
		aim = GetComponent<Animator>();
		bosslife = GetComponent<EnemyBossLife>();
		sr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		//transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, 1f * Time.deltaTime);
	}

	public void FaceToPlayer()
	{
		sr.flipX = Player.transform.position.x <= transform.position.x;
	}

	public void FireHorizon()
	{
		FaceToPlayer();
		//boss shoot one horizon fireball
		GameObject bulletNew = Instantiate(fireballHorizon, transform.position, transform.rotation, transform.parent);
		bulletNew.GetComponent<SpriteRenderer>().flipX = !sr.flipX;
		BossFireballHorizon fh = bulletNew.GetComponent<BossFireballHorizon>();
		fh.TargetPostion = new Vector3(transform.position.x + (sr.flipX ? -1f : 1f) * 20f, transform.position.y, transform.position.z);
		fh.MovingSpeed = 20f;
		fireballHorizonEffect.Play();
		//set state to run
		aim.SetInteger("state", 0);
	}
}
