using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAboriginalSmashBhv : StateMachineBehaviour
{
	private GameObject player;
	private BossAboriginalAI bossAI;

	Vector2 targetPos;
	bool hasDoneAction;
	private float bossSpeed;

	private bool isFalling;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player");
		targetPos = new Vector2(animator.transform.position.x, animator.transform.position.y + 7f);
		bossAI = animator.GetComponent<BossAboriginalAI>();
		bossSpeed = bossAI.GetBossScrollSpeed();
		hasDoneAction = false;
		isFalling = false;
		bossAI.FaceToPlayer();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (hasDoneAction) return;

		if (Vector2.Distance(animator.transform.position, targetPos) > 0.1f)
		{
			animator.transform.position = Vector2.MoveTowards(animator.transform.position, targetPos, bossSpeed * Time.deltaTime);
		}
		else
		{
			animator.transform.position = targetPos;
			if (!isFalling)
			{
				targetPos = new Vector2(animator.transform.position.x, 1.01f);
				isFalling = true;
			}
			else
			{
				bossAI.FaceToPlayer();
				hasDoneAction = true;
				//fire lava
				bossAI.FireLava();
				//set the smash
				animator.SetInteger("state", 5);
			}
		}
	}
}
