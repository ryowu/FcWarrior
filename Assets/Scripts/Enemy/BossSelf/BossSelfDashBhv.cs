using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSelfDashBhv : StateMachineBehaviour
{
	private GameObject player;
	private BossSelfAI bossAI;

	Vector2 targetPos;
	bool hasDoneAction;
	private float bossSpeed;
	private SpriteRenderer sr;

	[SerializeField] private GameObject BossSword;
	private GameObject newBossSword;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player");
		targetPos = new Vector2(player.transform.position.x, animator.transform.position.y);
		bossAI = animator.GetComponent<BossSelfAI>();
		bossSpeed = bossAI.GetBossDashSpeed();
		newBossSword = Instantiate(BossSword, animator.transform.position, animator.transform.rotation, animator.transform.parent);
		sr = newBossSword.GetComponent<SpriteRenderer>();
		newBossSword.SetActive(false);
		hasDoneAction = false;

		bossAI.FaceToPlayer();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (hasDoneAction) return;

		float delta;
		if (animator.transform.position.x > player.transform.position.x)
		{
			sr.flipX = true;
			delta = -1f;
		}
		else
		{
			sr.flipX = false;
			delta = 1f;
		}

		//Run to player position
		if (Vector2.Distance(animator.transform.position, targetPos) > 0.1f)
		{
			animator.transform.position = Vector2.MoveTowards(animator.transform.position, targetPos, bossSpeed * Time.deltaTime);
			newBossSword.SetActive(true);
			newBossSword.transform.position = new Vector2(animator.transform.position.x + delta, animator.transform.position.y);
		}
		else
		{
			newBossSword.SetActive(false);
			animator.SetInteger("state", 1);
			hasDoneAction = true;
		}
	}
}
