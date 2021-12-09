using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPlatformController : MonoBehaviour
{
	[SerializeField] private GameObject TargetPosition;
	[SerializeField] private bool DisappearOnArrival = true;
	[SerializeField] private float MovingSpeed = 2f;
	[SerializeField] private float Acceleration = 0f;
	bool startFloat;
	float actualSpeed;
	float acc;
	DateTime dtAcc;
	// Start is called before the first frame update
	void Start()
	{
		startFloat = false;
		actualSpeed = MovingSpeed;
		acc = Acceleration;
		dtAcc = DateTime.Now;
	}

	// Update is called once per frame
	void Update()
	{
		if (!startFloat) return;

		if (Vector2.Distance(transform.position, TargetPosition.transform.position) > 0.1f)
		{
			if (acc > 0f)
			{
				actualSpeed = MovingSpeed + acc;
				TimeSpan ts = DateTime.Now - dtAcc;
				if (ts.TotalMilliseconds > 200f)
				{
					acc += acc;
					dtAcc = DateTime.Now;
				}
			}
			transform.position = Vector2.MoveTowards(transform.position, TargetPosition.transform.position, actualSpeed * Time.deltaTime);
		}
		else
		{
			if (DisappearOnArrival)
			{
				Destroy(this.gameObject);
			}
			else
			{
				startFloat = false;
				transform.position = TargetPosition.transform.position;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			startFloat = true;
		}
	}
}
