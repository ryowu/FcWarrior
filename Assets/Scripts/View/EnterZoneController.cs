using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterZoneController : MonoBehaviour
{
	[SerializeField] private GameObject cameraMain;

	[SerializeField] private float targetX = 183.57f;
	[SerializeField] private float targetY = -16.63f;
	[SerializeField] private float movingSpeed = 8f;
	[SerializeField] private GameObject player;

	[SerializeField] private float boundXLeft = 5.56f;
	[SerializeField] private float boundXRight = 183.57f;
	[SerializeField] private float boundYTop = 44.1f;
	[SerializeField] private float boundYBottom = 5.48f;

	[SerializeField] private bool RestoreCameraFollowing = false;

	private Vector3 targetPos;

	private bool isAnimationWorking;
	private bool startCameraAnimation;

	private CameraController cameraController;

	// Start is called before the first frame update
	void Start()
	{
		startCameraAnimation = false;
		isAnimationWorking = true;
		targetPos = new Vector3(targetX, targetY, cameraMain.transform.position.z);
		cameraController = cameraMain.GetComponent<CameraController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!startCameraAnimation) return;
		if (!isAnimationWorking) return;
		if (GlobalVars.IsCameraFollowing) return;

		if (Vector3.Distance(targetPos, cameraMain.transform.position) > 0.1f)
		{
			cameraMain.transform.position = Vector3.MoveTowards(cameraMain.transform.position, targetPos, movingSpeed * Time.deltaTime);
		}
		else
		{
			cameraMain.transform.position = targetPos;
			isAnimationWorking = false;
			GlobalVars.IsPlayerControllable = true;
			player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

			if (RestoreCameraFollowing)
			{
				GlobalVars.IsCameraFollowing = true;
				cameraController.ResetToPosition(cameraMain.transform.position);
				cameraController.boundXLeft = boundXLeft;
				cameraController.boundXRight = boundXRight;
				cameraController.boundYBottom = boundYBottom;
				cameraController.boundYTop = boundYTop;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Make sure this trigger only works once
		if (startCameraAnimation) return;
		if (collision.gameObject.CompareTag("Player"))
		{
			player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			startCameraAnimation = true;
			GlobalVars.IsPlayerControllable = false;
			GlobalVars.IsCameraFollowing = false;
		}
	}
}
