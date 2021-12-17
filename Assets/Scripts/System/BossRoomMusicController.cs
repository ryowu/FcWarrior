using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomMusicController : MonoBehaviour
{
	private GameObject originalMusic;
	private AudioSource audioBGMusic;
	float deltaVolume;

	private bool isPlayingBossMusic;


	// Start is called before the first frame update
	void Start()
	{
		GlobalVars.BossAbnormalSequenceEvent.DisableOriginalBGM = true;

		isPlayingBossMusic = false;
		//GlobalVars.IsPlayerControllable = false;
		originalMusic = GameObject.FindGameObjectWithTag("bgmusic");

		if(originalMusic == null)
			originalMusic = GameObject.FindGameObjectWithTag("finalbossBGM");
		if (originalMusic != null)
			audioBGMusic = originalMusic.GetComponent<AudioSource>();
	}

	void Update()
	{
		if (isPlayingBossMusic || audioBGMusic == null) return;

		if (!GlobalVars.BossAbnormalSequenceEvent.DisableOriginalBGM) return;

		deltaVolume = 0.2f * Time.deltaTime;
		if (audioBGMusic.volume - deltaVolume > 0.01f)
			audioBGMusic.volume = audioBGMusic.volume - deltaVolume;
		else
		{
			isPlayingBossMusic = true;
			audioBGMusic.Stop();
			//Destroy(originalMusic);

			GlobalVars.BossAbnormalSequenceEvent.DisableOriginalBGM = false;
			GlobalVars.BossAbnormalSequenceEvent.PlayWarning = true;
}
	}
}
