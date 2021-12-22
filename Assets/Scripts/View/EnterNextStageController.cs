using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EnterNextStageController : MonoBehaviour
{
	[SerializeField] private Image FadeinoutImage;
	[SerializeField] private int NextStageIndex = -1;

	private Animator anim;
	private bool triggerStarted = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (triggerStarted) return;

		if (collision.CompareTag("Player"))
		{
			GlobalVars.IsPlayerControllable = false;
			collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			anim = FadeinoutImage.GetComponent<Animator>();
			FadeinoutImage.enabled = true;

			if (NextStageIndex >= 0)
				FadeinoutImage.GetComponent<FadeOutController>().StageSceneIndex = NextStageIndex;
			triggerStarted = true;
			anim.SetTrigger("fadeout");
		}
	}
}
