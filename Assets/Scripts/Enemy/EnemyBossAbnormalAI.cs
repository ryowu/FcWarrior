using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossAbnormalAI : PatrolBase
{
	[SerializeField] private GameObject player;
	[SerializeField] private bool IsFlip;

	[SerializeField] private GameObject fireballHorizon;
	[SerializeField] private GameObject fireballRound;

	[SerializeField] private AudioSource fireballHorizonEffect;

	private Animator anim;
	private SpriteRenderer sprite;
	private Vector3 targetPos;

	private enum BossState
	{
		Idle,
		Running,
		Jump,
		Fall
	}
	private BossState bossState;

	// Start is called before the first frame update
	void Start()
	{
		bossState = BossState.Idle;
		sprite = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}

	protected override void OnPatroling(Vector3 vFrom, Vector3 vTo)
	{
		if (vFrom.x <= vTo.x)
		{
			bossState = BossState.Running;
			sprite.flipX = IsFlip;
		}
		else if (vFrom.x > vTo.x)
		{
			bossState = BossState.Running;
			sprite.flipX = !IsFlip;
		}
		else
		{
			bossState = BossState.Idle;
		}

		//if (rb.velocity.y > .1f)
		//{
		//	bossState = BossState.Jump;
		//}
		//else if (rb.velocity.y < -.1f)
		//{
		//	bossState = BossState.Fall;
		//}

		anim.SetInteger("state", (int)bossState);
	}

	protected override bool BeforePatroling()
	{
		return GlobalVars.BossAbnormalSequenceEvent.StartAI;
	}

	protected override void OnPatrolPointArrived(Vector3 pos)
	{
		//trun around, face to player
		if (player.transform.position.x < transform.position.x)
		{
			sprite.flipX = true;
		}
		else
			sprite.flipX = false;

		bossState = BossState.Idle;
		anim.SetInteger("state", (int)bossState);

		//boss shoot one horizon fireball
		GameObject bulletNew = Instantiate(fireballHorizon, transform.position, transform.rotation, transform.parent);
		bulletNew.GetComponent<SpriteRenderer>().flipX = !sprite.flipX;
		BossFireballHorizon fh = bulletNew.GetComponent<BossFireballHorizon>();
		fh.TargetPostion = new Vector3(transform.position.x + (sprite.flipX ? -1f : 1f) * 20f, transform.position.y, transform.position.z);
		fh.MovingSpeed = 20f;
		fireballHorizonEffect.Play();

		WaitTime = 2000;
		startWait = true;
	}
}
