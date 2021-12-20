using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAboriginalRunBhv : StateMachineBehaviour
{
	private GameObject player;
	private GameObject bossShell;
	private EnemyData bossData;
	//private bool isArrivedTargetPos;
	Vector2 targetPos;

	private enum ActionState
	{
		FireAttack,
		FireBallDrop
	}

	private ActionState bossAction;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player");
		//isArrivedTargetPos = true;
		bossAction = ActionState.FireAttack;
		targetPos = player.transform.position;
		bossData = animator.GetComponent<EnemyData>();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		switch (bossAction)
		{
			case ActionState.FireAttack:
				{
					if (Vector2.Distance(animator.transform.position, targetPos) > 0.1f)
					{
						animator.transform.position = Vector2.MoveTowards(animator.transform.position, targetPos, bossData.BossSpeed * Time.deltaTime);
					}
					else
					{
						animator.transform.position = targetPos;
						//animator.SetTrigger("fireHorizon");
					}
					break;
				}
			case ActionState.FireBallDrop:
				{

					break;
				}
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
}
