using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPinkyRunBhv : StateMachineBehaviour
{
	private BossPinkyAI bossAI;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		bossAI = animator.GetComponent<BossPinkyAI>();

		//Show rage
		if (bossAI.IsRage && !bossAI.RageShown)
		{
			bossAI.RageShown = true;
			bossAI.SetImmune(true);
			animator.SetTrigger("bossrage");
			return;
		}

		switch (bossAI.GetNextAction())
		{
			case "jumpSide":
				{
					//set to jump
					animator.SetInteger("state", 2);
					break;
				}
			case "jumpCenter":
				{
					//set to jump
					animator.SetInteger("state", 8);
					break;
				}
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
}
