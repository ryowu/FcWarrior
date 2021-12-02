using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private float movingSpeed = 3f;
	[SerializeField] private bool flipping;
	public Vector3 MonitorTargetPostion;
	public bool IsReturning;

	private Vector3 originalPosition;


	private Animator anim;
	private enum MovementState
	{
		idle, flying, hit
	}
	MovementState state;
	private SpriteRenderer sprite;

	private EnemyLife enemyLifeObject;

	void Start()
	{
		IsReturning = false;
		originalPosition = transform.position;
		MonitorTargetPostion = transform.position;

		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();

		enemyLifeObject = GetComponent<EnemyLife>();
	}

	// Update is called once per frame
	void Update()
	{
		if (enemyLifeObject != null && !enemyLifeObject.IsAlive) return;

		if (Vector3.Distance(MonitorTargetPostion, transform.position) > 0.1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, MonitorTargetPostion, movingSpeed * Time.deltaTime);
			sprite.flipX = MonitorTargetPostion.x > transform.position.x;
			state = MovementState.flying;

		}
		else
		{
			if (!IsReturning)
			{
				IsReturning = true;
				MonitorTargetPostion = originalPosition;
			}
			else
				transform.position = MonitorTargetPostion;
			state = MovementState.idle;
		}

		anim.SetInteger("state", (int)state);
	}
}
