using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSelfJumpBhv : StateMachineBehaviour
{
	private GameObject player;
	private BossSelfAI bossAI;

	Vector2 targetPos;
	bool hasDoneAction;
	private float bossSpeed;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player");
		if(player.transform.position.x < -1.77f)
			targetPos = new Vector2(-11.61f, 6.69f);
		else
			targetPos = new Vector2(8.74f, 6.69f);
		bossAI = animator.GetComponent<BossSelfAI>();
		bossSpeed = bossAI.GetBossDashSpeed();
		hasDoneAction = false;

		bossAI.FaceToPlayer();
	}

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
			animator.SetInteger("state", 5);

			hasDoneAction = true;
		}
	}
}
