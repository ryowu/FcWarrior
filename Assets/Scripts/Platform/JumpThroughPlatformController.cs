using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpThroughPlatformController : MonoBehaviour
{
	private PlatformEffector2D effector;
	private float waitTime;
	private float dirY;

	private bool playerOnPlatform;

	// Start is called before the first frame update
	void Start()
	{
		effector = GetComponent<PlatformEffector2D>();
		playerOnPlatform = false;
	}

	// Update is called once per frame
	void Update()
	{
		dirY = Input.GetAxisRaw("Vertical");
		if (dirY < 0 && (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.K)))
		{
			if (playerOnPlatform)
			{
				effector.rotationalOffset = 180f;
			}
			else
				effector.rotationalOffset = 0f;
		}
		else if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.K))
		{
			effector.rotationalOffset = 0f;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			playerOnPlatform = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			playerOnPlatform = false;
		}
	}
}
