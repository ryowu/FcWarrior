using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningController : MonoBehaviour
{
	[SerializeField] private AudioSource warningEffect;

	DateTime startTime;
	bool startWait;
	private void Start()
	{
		startWait = false;
	}

	private void Update()
	{
		if (!GlobalVars.BossAbnormalSequenceEvent.PlayWarning) return;

		if (startWait)
		{
			TimeSpan ts = DateTime.Now - startTime;
			if (ts.TotalSeconds > 4)
			{
				GlobalVars.BossAbnormalSequenceEvent.PlayWarning = false;
				GlobalVars.BossAbnormalSequenceEvent.BossShowUp = true;
			}
		}
		else
		{
			startWait = true;
			startTime = DateTime.Now;
			warningEffect.Play();
		}
	}
}
