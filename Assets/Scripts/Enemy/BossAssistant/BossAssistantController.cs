using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAssistantController : MonoBehaviour
{
	[SerializeField] public GameObject Boss;
	[SerializeField] public GameObject HPBar;
	[SerializeField] public GameObject DialogArea;
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
}
