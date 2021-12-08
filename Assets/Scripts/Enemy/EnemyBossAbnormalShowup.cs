using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossAbnormalShowup : MonoBehaviour
{
	[SerializeField] private GameObject EnemyBoss;

	private Animator anim;
	Vector3 TargetPos;
	// Start is called before the first frame update
	void Start()
	{
		TargetPos = new Vector3(6.78f, 0.95f, 0f);
		anim = EnemyBoss.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!GlobalVars.BossAbnormalSequenceEvent.BossShowUp) return;

		anim.SetInteger("state", 3);//set fall
		EnemyBoss.transform.position = Vector2.MoveTowards(EnemyBoss.transform.position, TargetPos, 12f * Time.deltaTime);

		if (Vector2.Distance(TargetPos, EnemyBoss.transform.position) < 0.1f)
		{
			EnemyBoss.transform.position = TargetPos;
			anim.SetInteger("state", 0);

			GlobalVars.BossAbnormalSequenceEvent.BossShowUp = false;
			GlobalVars.BossAbnormalSequenceEvent.BossHPShowUp = true;
		}
	}
}
