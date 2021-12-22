using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMoving : MonoBehaviour
{
	public Vector3 TargetPostion;
	private float movingSpeed;
	private SpriteRenderer sr;

	void Start()
	{
		movingSpeed = GetComponent<FireballData>().Speed;
		sr = GetComponent<SpriteRenderer>();
		sr.flipX = TargetPostion.x <= transform.position.x;
	}

	// Update is called once per frame
	void Update()
	{
		//Destroy when out of camera
		if (!sr.isVisible) Destroy(this.gameObject);
		

		if (Vector3.Distance(TargetPostion, transform.position) > 0.1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, TargetPostion, movingSpeed * Time.deltaTime);
		}
		else
		{
			transform.position = TargetPostion;
			Destroy(this.gameObject);
		}
	}
}
