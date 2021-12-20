using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireballRound : MonoBehaviour
{
	public Vector2 TargetPostion;
	public float MovingSpeed;

	private int movingState;

	// Start is called before the first frame update
	void Start()
	{
		movingState = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (Vector2.Distance(TargetPostion, transform.position) > 0.1f)
		{
			transform.position = Vector2.MoveTowards(transform.position, TargetPostion, MovingSpeed * Time.deltaTime);
		}
		else
		{
			transform.position = TargetPostion;

			if (movingState == 0)
			{
				movingState = 1;
				MovingSpeed = 8f;
				TargetPostion = new Vector2(transform.position.x, -5f);
			}
			else if (movingState == 1)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
