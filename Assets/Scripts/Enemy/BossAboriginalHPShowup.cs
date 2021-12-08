using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAboriginalHPShowup : MonoBehaviour
{
	[SerializeField] private GameObject enemyBoss;
	[SerializeField] private AudioSource hpRestoreEffect;

	private EnemyBossLife bosslife;

	DateTime startTime;
	bool startWait;

	float hp;
	private void Start()
	{
		hp = 0;
		startWait = false;
		bosslife = enemyBoss.GetComponent<EnemyBossLife>();
	}

	private void Update()
	{
		if (!GlobalVars.BossAbnormalSequenceEvent.BossHPShowUp) return;

		if (startWait)
		{
			TimeSpan ts = DateTime.Now - startTime;
			if (ts.TotalSeconds < 1)
			{
				return;
			}
		}
		else
		{
			startWait = true;
			startTime = DateTime.Now;
		}

		if (hp <= 300f)
		{
			hp += 120f * Time.deltaTime;
			hpRestoreEffect.Play();
			bosslife.healthyBar.SetHPValue((int)hp);
		}
		else
		{
			bosslife.healthyBar.SetHPValue(300);
			GlobalVars.BossAbnormalSequenceEvent.BossHPShowUp = false;
			GlobalVars.BossAbnormalSequenceEvent.PlayBossBGM = true;

			GlobalVars.IsPlayerControllable = true;
		}
	}
}
