using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPshowup : StateMachineBehaviour
{
	private GameObject boss;
	private EnemyBossLife bosslife;
	private DateTime soundStartTime;
	private EnemyData eData;
	float hp;
	bool hpSetDone;

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		hp = 0;
		boss = animator.GetComponent<BossAssistantController>().Boss;
		bosslife = boss.GetComponent<EnemyBossLife>();
		bosslife.healthyBar.SetHPValue(0);
		//show HP bar
		animator.GetComponent<BossAssistantController>().HPBar.SetActive(true);
		eData = boss.GetComponent<EnemyData>();
		soundStartTime = DateTime.Now;
		hpSetDone = false;
	}

	//OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (hpSetDone) return;

		if (hp <= eData.EnemyMaxHP)
		{
			hp += 300f * Time.deltaTime;
			TimeSpan ts = DateTime.Now - soundStartTime;
			if (ts.TotalMilliseconds > 50f)
			{
				bosslife.HPrestoreEffect.Play();
				soundStartTime = DateTime.Now;
			}
			bosslife.healthyBar.SetHPValue((int)hp);
		}
		else
		{
			bosslife.healthyBar.SetHPValue(eData.EnemyMaxHP);
			hpSetDone = true;

			//enable player control
			GlobalVars.IsPlayerControllable = true;
			//set state to run
			GameObject boss = animator.GetComponent<BossAssistantController>().Boss;
			Animator anim = boss.GetComponent<Animator>();
			anim.SetInteger("state", 1);
			Destroy(animator);

			//animator.SetTrigger("landdown");
		}
	}
}
