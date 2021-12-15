using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[SerializeField] private Image FadeinoutImage;
	[SerializeField] private AudioSource ItemSelect;
	[SerializeField] private Text LoadingLabel;
	private Animator anim;
	private bool triggerStarted = false;

	private void Start()
	{
		Cursor.visible = false;
		LoadingLabel.enabled = false;
	}

	// Update is called once per frame
	void Update()
    {
		if (triggerStarted) return;

		if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.J)
			|| Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
		{
			ItemSelect.Play();
			anim = FadeinoutImage.GetComponent<Animator>();
			FadeinoutImage.enabled = true;
			triggerStarted = true;
			anim.SetTrigger("fadeout");
			LoadingLabel.enabled = true;
		}
	}
}
