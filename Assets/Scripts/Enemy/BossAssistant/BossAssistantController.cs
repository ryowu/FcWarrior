using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAssistantController : MonoBehaviour
{
	[SerializeField] public GameObject Boss;
	[SerializeField] public GameObject HPBar;
	[SerializeField] public GameObject DialogArea;
	[SerializeField] private AudioSource BGM_NormalBoss;
	[SerializeField] private AudioSource BGM_FinalBoss;
	[SerializeField] private AudioSource BGM_FinalWords;

	private Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
		Boss.GetComponent<EnemyBossLife>().healthyBar.SetHPMaxValue(0);
		Boss.GetComponent<EnemyBossLife>().healthyBar.SetHPValue(0);
		HPBar.SetActive(false);
	}

	public void OnWarningComplete()
	{
		anim.ResetTrigger("warning");
		anim.SetTrigger("showup");
	}

	public void PlayBGMNormal()
	{
		BGM_NormalBoss.Play();
	}

	public void PlayBGMFinal()
	{
		BGM_FinalBoss.Play();
	}

	public void StopPlayBossBGM()
	{
		if (BGM_FinalBoss.isPlaying)
		{
			BGM_FinalBoss.Stop();
			BGM_FinalWords.Play();
		}
	}
}
