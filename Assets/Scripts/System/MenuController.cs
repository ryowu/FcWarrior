using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[SerializeField] private Image FadeinoutImage;
	[SerializeField] private AudioSource ItemSelect;
	private Animator anim;
	private bool triggerStarted = false;

	private void Start()
	{
		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update()
    {
		if (triggerStarted) return;

		if (Input.GetButtonDown("Fire2"))
		{
			ItemSelect.Play();
			anim = FadeinoutImage.GetComponent<Animator>();
			FadeinoutImage.enabled = true;
			triggerStarted = true;
			anim.SetTrigger("fadeout");
		}
	}
}
