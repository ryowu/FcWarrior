using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBulletController : MonoBehaviour
{
	public Vector2 TargetPostion;
	EnemyData edata;

	// Start is called before the first frame update
	void Start()
	{
		edata = GetComponent<EnemyData>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Vector2.Distance(TargetPostion, transform.position) > 0.1f)
		{
			transform.position = Vector2.MoveTowards(transform.position, TargetPostion, edata.BossSpeed * Time.deltaTime);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
