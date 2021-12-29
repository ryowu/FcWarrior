using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSelfRunBhv : StateMachineBehaviour
{
	private BossSelfAI bossAI;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		bossAI = animator.GetComponent<BossSelfAI>();

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
			case "Shoot":
				{
					animator.SetInteger("state", 2);
					break;
				}
			case "Rush":
				{
					animator.SetInteger("state", 3);
					break;
				}
			case "JumpShoot":
				{
					animator.SetInteger("state", 4);
					break;
				}
		}
	}
}
