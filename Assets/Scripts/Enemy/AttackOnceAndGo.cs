using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOnceAndGo : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private float movingSpeed;

	private bool foundTarget;
	private bool startLeft;
	private Vector2 targetPos;
	private EnemyLife enemyLifeObject;

	// Start is called before the first frame update
	void Start()
	{
		foundTarget = false;
		startLeft = false;
		enemyLifeObject = GetComponent<EnemyLife>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!enemyLifeObject.IsAlive) return;

		if (!foundTarget && Vector2.Distance(player.transform.position, transform.position) < 20f)
		{
			targetPos = player.transform.position;
			foundTarget = true;
		}

		if (foundTarget)
		{
			if (Vector2.Distance(transform.position, targetPos) > 0.1f)
			{
				transform.position = Vector2.MoveTowards(transform.position, targetPos, movingSpeed * Time.deltaTime);
			}
			else
			{
				if (startLeft)
				{
					Destroy(this.gameObject);
				}
				else
				{
					startLeft = true;
					targetPos = new Vector2(transform.position.x, transform.position.y + 30f);
				}
			}
		}
	}
}
