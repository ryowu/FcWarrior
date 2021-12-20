using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFrogRunBhv : StateMachineBehaviour
{
	private GameObject player;
	private BossFrogAI bossAI;

	Vector2 targetPos;
	bool hasDoneAction;
	private float bossSpeed;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player");
		targetPos = new Vector2(player.transform.position.x, animator.transform.position.y);
		bossAI = animator.GetComponent<BossFrogAI>();
		bossSpeed = bossAI.GetBossSpeed();
		hasDoneAction = false;

		bossAI.FaceToPlayer();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (hasDoneAction) return;

		//Show rage
		if (bossAI.IsRage && !bossAI.RageShown)
		{
			bossAI.RageShown = true;
			bossAI.SetImmune(true);
			hasDoneAction = true;
			animator.SetTrigger("bossrage");
			return;
		}

		//Run to player position
		if (Vector2.Distance(animator.transform.position, targetPos) > 0.1f)
		{
			animator.transform.position = Vector2.MoveTowards(animator.transform.position, targetPos, bossSpeed * Time.deltaTime);
		}
		else
		{
			animator.transform.position = targetPos;
			bossAI.FaceToPlayer();
			animator.SetTrigger(bossAI.GetFireAction());

			hasDoneAction = true;
		}


	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//animator.SetInteger("state", 1);
	}
}
