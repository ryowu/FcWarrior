using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInController : MonoBehaviour
{
	private Animator anim;

	// Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();
		anim.SetTrigger("fadein");
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnFadeInComplete()
	{
		//this.gameObject.SetActive(false);
	}
}
