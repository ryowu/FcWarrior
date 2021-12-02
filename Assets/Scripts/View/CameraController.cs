using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float boundXLeft = 5.56f;
	public float boundXRight = 183.57f;
	public float boundYTop = 44.1f;
	public float boundYBottom = 5.48f;

	private float posX;
	private float posY;

	[SerializeField] private Transform player;
	[SerializeField] private Transform backgroundImage;

	private void Start()
	{
		posX = transform.position.x;
		posY = transform.position.y;
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
		if (player.position.x > boundXLeft &&
	player.position.x < boundXRight)
		{
			posX = player.position.x;
		}

		if (player.position.y > boundYBottom &&
			player.position.y < boundYTop)
		{
			posY = player.position.y;
		}

		if (GlobalVars.IsCameraFollowing)
		{
			transform.position = new Vector3(posX, posY, transform.position.z);
		}

		backgroundImage.position = new Vector3(transform.position.x, transform.position.y, 0f);
	}
}
