using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBossMusic : MonoBehaviour
{
	[SerializeField] private AudioSource bossBgm;

	private void Update()
	{
		if (GlobalVars.BossAbnormalSequenceEvent.StopBossBGM)
		{
			GlobalVars.IsPlayerControllable = false;
			bossBgm.Stop();
			Destroy(this.gameObject);
		}

		if (!GlobalVars.BossAbnormalSequenceEvent.PlayBossBGM) return;

		GlobalVars.BossAbnormalSequenceEvent.PlayBossBGM = false;
		bossBgm.Play();

		GlobalVars.BossAbnormalSequenceEvent.StartAI = true;
	}
}
