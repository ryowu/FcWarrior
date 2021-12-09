using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearController : MonoBehaviour
{
	[SerializeField] private Image FadeinoutImage;

	private Animator anim;
	DateTime startTime;
	bool startWait;
	bool isAnimating;

	private void Start()
	{
		startWait = false;
		isAnimating = false;
	}

	private void Update()
	{
		if (!GlobalVars.BossAbnormalSequenceEvent.StopBossBGM) return;

		if (startWait)
		{
			TimeSpan ts = DateTime.Now - startTime;
			if (ts.TotalSeconds > 2 && !isAnimating)
			{
				//dispose original bgm object
				GameObject bgmobject = GameObject.FindGameObjectWithTag("bgmusic");
				if (bgmobject != null)
					Destroy(bgmobject);

				anim = FadeinoutImage.GetComponent<Animator>();
				FadeinoutImage.enabled = true;
				isAnimating = true;
				anim.SetTrigger("fadeout");
			}
		}
		else
		{
			startWait = true;
			startTime = DateTime.Now;
		}
	}
}
