using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossAbnormalAI : PatrolBase
{
	[SerializeField] private GameObject player;
	[SerializeField] private bool IsFlip;
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
		//	state = MovementState.jumping;
		//}
		//else if (rb.velocity.y < -.1f)
		//{
		//	state = MovementState.falling;
		//}

		anim.SetInteger("state", (int)bossState);
	}

	protected override bool BeforePatroling()
	{
		return base.BeforePatroling();
	}
}
