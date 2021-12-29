using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSelfFallBhv : StateMachineBehaviour
{
	private GameObject player;
	private BossSelfAI bossAI;

	Vector2 targetPos;
	bool hasDoneAction;
	private float bossSpeed;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player");
		targetPos = new Vector2(player.transform.position.x, 1.01f);
		bossAI = animator.GetComponent<BossSelfAI>();
		bossSpeed = bossAI.GetBossDashSpeed();
		hasDoneAction = false;

		bossAI.FaceToPlayer();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (hasDoneAction) return;

		//Run to player position
		if (Vector2.Distance(animator.transform.position, targetPos) > 0.1f)
		{
			animator.transform.position = Vector2.MoveTowards(animator.transform.position, targetPos, bossSpeed * Time.deltaTime);
		}
		else
		{
			animator.SetInteger("state", 1);

			hasDoneAction = true;
		}
	}
}
