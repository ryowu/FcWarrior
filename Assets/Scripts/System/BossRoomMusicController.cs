using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomMusicController : MonoBehaviour
{
	[SerializeField] private AudioSource bossMusic;

	private GameObject originalMusic;
	private AudioSource audioBGMusic;
	float deltaVolume;

	private bool isPlayingBossMusic;
	// Start is called before the first frame update
	void Start()
	{
		isPlayingBossMusic = false;
		//GlobalVars.IsPlayerControllable = false;
		originalMusic = GameObject.FindGameObjectWithTag("bgmusic");
		if (originalMusic != null)
			audioBGMusic = originalMusic.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if (isPlayingBossMusic || audioBGMusic == null) return;

		deltaVolume = 0.2f * Time.deltaTime;
		if (audioBGMusic.volume - deltaVolume > 0.01f)
			audioBGMusic.volume = audioBGMusic.volume - deltaVolume;
		else
		{
			isPlayingBossMusic = true;
			GlobalVars.IsPlayerControllable = true;
			audioBGMusic.Stop();
			bossMusic.Play();
		}
	}
}
