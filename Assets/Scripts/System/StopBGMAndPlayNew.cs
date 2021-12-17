using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBGMAndPlayNew : MonoBehaviour
{
	[SerializeField] private AudioSource newBGM;

	private bool foundOriginalMusic;
	GameObject bgmobject;
	// Start is called before the first frame update
	void Start()
	{
		foundOriginalMusic = false;
		//dispose original bgm object
	}

	private void Update()
	{
		if (foundOriginalMusic) return;

		bgmobject = GameObject.FindGameObjectWithTag("bgmusic");
		if (bgmobject != null)
		{
			Destroy(bgmobject);
			newBGM.Play();
			foundOriginalMusic = true;
			DontDestroyOnLoad(this.gameObject);
		}
	}
}
