using UnityEngine;

public class EnemyPatrol : PatrolBase
{
	private EnemyLife enemyLifeObject;
	private SpriteRenderer sprite;
	private Animator anim;
	private enum MovementState { idle, running, jumping, falling }

	[SerializeField] private bool IsFlip;

	protected override bool BeforePatroling()
	{
		return enemyLifeObject != null && enemyLifeObject.IsAlive;
	}

	protected override void OnPatroling(Vector3 vFrom, Vector3 vTo)
	{
		MovementState state;

		if (vFrom.x <= vTo.x)
		{
			state = MovementState.running;
			sprite.flipX = IsFlip;
		}
		else if (vFrom.x > vTo.x)
		{
			state = MovementState.running;
			sprite.flipX = !IsFlip;
		}
		else
		{
			state = MovementState.idle;
		}

		//if (rb.velocity.y > .1f)
		//{
		//	state = MovementState.jumping;
		//}
		//else if (rb.velocity.y < -.1f)
		//{
		//	state = MovementState.falling;
		//}

		anim.SetInteger("state", (int)state);
	}

	// Start is called before the first frame update
	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		enemyLifeObject = GetComponent<EnemyLife>();
	}
}
