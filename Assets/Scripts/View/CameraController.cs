using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private bool UseNaturalFollowing = false;

	public float boundXLeft = -0.49f;
	public float boundXRight = 183.57f;
	public float boundYTop = 35.1f;
	public float boundYBottom = 2.2f;

	public float xOffset = 0f;
	public float yOffset = 0f;

	private float posX;
	private float posY;

	public GameObject followTarget;

	private void Start()
	{
		posX = transform.position.x;
		posY = transform.position.y;

		//Player is the default follow object
		followTarget = GameObject.FindGameObjectWithTag("Player");
		//UpdateCameraPosition();
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
		//	if (followTarget.transform.position.x > boundXLeft &&
		//followTarget.transform.position.x < boundXRight)
		//	{
		//		posX = followTarget.transform.position.x;
		//	}

		//	if (followTarget.transform.position.y > boundYBottom &&
		//		followTarget.transform.position.y < boundYTop)
		//	{
		//		posY = followTarget.transform.position.y;
		//	}

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
			//if (UseNaturalFollowing)
				transform.position = newPos;
			//else
			//	transform.position = Vector3.MoveTowards(transform.position, newPos, 8f * Time.deltaTime);
		}
	}
}
