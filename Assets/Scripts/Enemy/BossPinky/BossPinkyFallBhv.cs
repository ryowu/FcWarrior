using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPinkyFallBhv : StateMachineBehaviour
{
	private GameObject player;
	private BossPinkyAI bossAI;

	Vector2 targetPos;
	bool hasDoneAction;
	private float bossSpeed;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player");
		targetPos = new Vector2(animator.transform.position.x, 1.01f);


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
			//set to run
			animator.SetInteger("state", 1);

		}
	}
}
