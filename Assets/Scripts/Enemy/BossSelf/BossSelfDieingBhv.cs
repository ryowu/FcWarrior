using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSelfDieingBhv : StateMachineBehaviour
{
	private BossSelfAI bossAI;

	Vector2 targetPos;
	bool hasDoneAction;
	private float bossSpeed;
	DateTime dtStart;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		GlobalVars.IsPlayerControllable = false;
		targetPos = new Vector2(animator.transform.position.x, 0.1f);
		bossAI = animator.GetComponent<BossSelfAI>();
		bossSpeed = 1f;
		hasDoneAction = false;
		dtStart = DateTime.Now;
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (hasDoneAction) return;

		//fall to ground position
		if (Vector2.Distance(animator.transform.position, targetPos) > 0.1f)
		{
			animator.transform.position = Vector2.MoveTowards(animator.transform.position, targetPos, bossSpeed * Time.deltaTime);
		}
		else
		{
			TimeSpan ts = DateTime.Now - dtStart;
			if (ts.TotalMilliseconds < 3500) return;

			GameObject gob = GameObject.FindGameObjectWithTag("bossAssistant");
			if (gob != null)
				gob.GetComponent<BossAssistantController>().StopPlayBossBGM();

			bossAI.ShowFinalDialog();
			hasDoneAction = true;
		}
	}
}
