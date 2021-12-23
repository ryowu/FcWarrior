using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPinkyJumpBhv : StateMachineBehaviour
{
	private GameObject player;
	private BossPinkyAI bossAI;

	Vector2 targetPos;
	bool hasDoneAction;
	private float bossSpeed;

	private float leftX = -11.7f;
	private float rightX = 8.56f;
	private float jumpHeight = 9.72f;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player");

		if (player.transform.position.x < animator.transform.position.x)
			targetPos = new Vector2(leftX, jumpHeight);
		else
			targetPos = new Vector2(rightX, jumpHeight);

		bossAI = animator.GetComponent<BossPinkyAI>();
		bossSpeed = bossAI.GetBossScrollSpeed();
		hasDoneAction = false;
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

			bossAI.FaceToPlayer();
			hasDoneAction = true;

			animator.SetInteger("state", 7);

		}
	}
}
