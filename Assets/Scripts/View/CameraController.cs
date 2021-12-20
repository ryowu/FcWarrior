using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private GameObject bgImage;

	[SerializeField] private float bgImageOffsetX;
	[SerializeField] private float bgImageOffsetY;

	public float boundXLeft = -0.49f;
	public float boundXRight = 183.57f;
	public float boundYTop = 35.1f;
	public float boundYBottom = 2.2f;

	public float xOffset = 0f;
	public float yOffset = 0f;

	private float posX;
	private float posY;

	public GameObject followTarget;

	private Camera cameraMain;
	Vector3 newPosWorldToScreen;

	private void Start()
	{
		posX = transform.position.x;
		posY = transform.position.y;

		//Player is the default follow object
		if (followTarget == null)
			followTarget = GameObject.FindGameObjectWithTag("Player");
		//UpdateCameraPosition();

		cameraMain = GetComponent<Camera>();
	}

	private void Update()
	{
		UpdateCameraPosition();
	}

	public void ResetToPosition(Vector3 pos)
	{
		posX = pos.x;
		posY = pos.y;
		UpdateCameraPosition();
	}

	private void UpdateCameraPosition()
	{
		posX = followTarget.transform.position.x;
		posY = followTarget.transform.position.y;

		if (posX < boundXLeft)
			posX = boundXLeft;
		else if (posX > boundXRight)
			posX = boundXRight;

		if (posY > boundYTop)
			posY = boundYTop;
		else if (posY < boundYBottom)
			posY = boundYBottom;

		if (GlobalVars.IsCameraFollowing)
		{
			Vector3 newPos = new Vector3(posX + xOffset, posY + yOffset, transform.position.z);
			transform.position = newPos;
		}

		if (bgImage != null)
			bgImage.transform.position = new Vector3(transform.position.x + bgImageOffsetX, transform.position.y + bgImageOffsetY, 0f);
	}
}
