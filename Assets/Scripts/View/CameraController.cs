using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float boundXLeft = -0.49f;
	public float boundXRight = 183.57f;
	public float boundYTop = 35.1f;
	public float boundYBottom = 2.2f;

	private float posX;
	private float posY;

	private GameObject player;
	[SerializeField] private Transform backgroundImage;

	private void Start()
	{
		posX = transform.position.x;
		posY = transform.position.y;

		player = GameObject.FindGameObjectWithTag("Player");
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
		if (player.transform.position.x > boundXLeft &&
	player.transform.position.x < boundXRight)
		{
			posX = player.transform.position.x;
		}

		if (player.transform.position.y > boundYBottom &&
			player.transform.position.y < boundYTop)
		{
			posY = player.transform.position.y;
		}

		if (GlobalVars.IsCameraFollowing)
		{
			transform.position = new Vector3(posX, posY, transform.position.z);
		}

		backgroundImage.position = new Vector3(transform.position.x, transform.position.y, 0f);
	}
}
