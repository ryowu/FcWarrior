using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBGMAndPlayNew : MonoBehaviour
{
	[SerializeField] private AudioSource newBGM;
	[SerializeField] private bool DestroyOld = true;
	[SerializeField] private bool KeepNew = true;
	private bool foundOriginalMusic;
	GameObject bgmobject;
	// Start is called before the first frame update
	void Start()
	{
		foundOriginalMusic = false;
		bgmobject = GameObject.FindGameObjectWithTag("bgmusic");
		//dispose original bgm object
	}

	private void Update()
	{
		if (foundOriginalMusic) return;

		
		if (bgmobject != null)
		{
			if (DestroyOld)
				Destroy(bgmobject);
			else
			{
				bgmobject.GetComponent<AudioSource>().Stop();
			}
			newBGM.Play();
			foundOriginalMusic = true;
			if (KeepNew)
				DontDestroyOnLoad(this.gameObject);
		}
	}
}
