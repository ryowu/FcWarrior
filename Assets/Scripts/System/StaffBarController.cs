using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffBarController : MonoBehaviour
{
	[SerializeField] private int TopPosition = 147;
	[SerializeField] private float ScrollingSpeed = 2f;

	private bool scrollDone = false;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (scrollDone) return;

		if (transform.position.y < TopPosition)
		{
			transform.position = new Vector2(transform.position.x, transform.position.y + ScrollingSpeed * Time.deltaTime);
		}
	}
}
