using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAboriginalScrollBhv : StateMachineBehaviour
{
	private GameObject player;
	private BossAboriginalAI bossAI;

	Vector2 targetPos;
	bool hasDoneAction;
	private float bossSpeed;
	private int scrollCount;

	private float deltaX = 4f;
	private float topY = 12.18f;
	private float bottomY = 1.01f;
	private float leftX = -11.66f;
	private float rightX = 8.89f;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player");
		targetPos = new Vector2(animator.transform.position.x - deltaX, topY);
		bossAI = animator.GetComponent<BossAboriginalAI>();
		bossSpeed = bossAI.GetBossScrollSpeed();
		hasDoneAction = false;
		scrollCount = 0;
		bossAI.FaceToPlayer();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (hasDoneAction) return;

		if (animator.transform.position.x < leftX)
		{
			targetPos = new Vector2(targetPos.x + (animator.transform.position.x - targetPos.x) * 2, targetPos.y);
			bossAI.FlipX = false;
		}
		else if (animator.transform.position.x > rightX)
		{
			bossAI.FlipX = true;
			targetPos = new Vector2(targetPos.x - (targetPos.x - animator.transform.position.x) * 2, targetPos.y);
		}

		//Run to player position
		if (Vector2.Distance(animator.transform.position, targetPos) > 0.1f)
		{
			animator.transform.position = Vector2.MoveTowards(animator.transform.position, targetPos, bossSpeed * Time.deltaTime);
			animator.SetInteger("state", 5);
		}
		else
		{
			scrollCount++;
			animator.transform.position = targetPos;
			if (scrollCount >= 8 && animator.transform.position.y < 2f)
			{
				hasDoneAction = true;
				//set to run
				animator.SetInteger("state", 1);
			}
			else
			{
				if (animator.transform.position.y > topY - 1f)
				{
					targetPos = new Vector2(animator.transform.position.x + (bossAI.FlipX ? -1f : 1f) * deltaX, bottomY);
				}
				else
				{
					targetPos = new Vector2(animator.transform.position.x + (bossAI.FlipX ? -1f : 1f) * deltaX, topY);
				}
			}
		}
	}
}
