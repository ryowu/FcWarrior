using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EnterNextStageController : MonoBehaviour
{
	[SerializeField] private Image FadeinoutImage;

	private Animator anim;
	private bool triggerStarted = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (triggerStarted) return;

		if (collision.CompareTag("Player"))
		{
			GlobalVars.IsPlayerControllable = false;

			anim = FadeinoutImage.GetComponent<Animator>();
			FadeinoutImage.enabled = true;
			triggerStarted = true;
			anim.SetTrigger("fadeout");
		}
	}
}
