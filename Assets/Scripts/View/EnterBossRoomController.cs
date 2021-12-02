using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBossRoomController : MonoBehaviour
{
	[SerializeField] private GameObject camera;
	[SerializeField] private GameObject bgMusic;

	private AudioSource audioBGMusic;
	[SerializeField] private AudioSource audioBossMusic;

	private const float targetX = 351.13f;
	private const float targetY = -60.21f;

	private const float movingSpeed = 8f;

	private Vector3 targetPos;

	private bool isAnimationWorking;


	private bool startCameraAnimation;
	// Start is called before the first frame update
	void Start()
	{
		startCameraAnimation = false;
		isAnimationWorking = true;
		targetPos = new Vector3(targetX, targetY, camera.transform.position.z);
		audioBGMusic = bgMusic.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!startCameraAnimation) return;
		if (!isAnimationWorking) return;
		if (GlobalVars.IsCameraFollowing) return;

		if (Vector3.Distance(targetPos, camera.transform.position) > 0.1f)
		{
			camera.transform.position = Vector3.MoveTowards(camera.transform.position, targetPos, movingSpeed * Time.deltaTime);
		}
		else
		{
			camera.transform.position = targetPos;
		}


		float deltaVolume = 0.1f * Time.deltaTime;
		if (audioBGMusic.volume - deltaVolume > 0.01f)
			audioBGMusic.volume = audioBGMusic.volume - deltaVolume;
		else
		{
			isAnimationWorking = false;
			GlobalVars.IsPlayerControllable = true;
			audioBGMusic.Stop();
			audioBossMusic.Play();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			startCameraAnimation = true;
			GlobalVars.IsPlayerControllable = false;
			GlobalVars.IsCameraFollowing = false;
		}
	}
}
