using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMoving : MonoBehaviour
{
	public Vector3 TargetPostion;
	private float movingSpeed;
	private SpriteRenderer sr;
	public bool hasDoneAction;

	private void Start()
	{
		hasDoneAction = false;
		movingSpeed = GetComponent<FireballData>().Speed;
		sr = GetComponent<SpriteRenderer>();
		sr.flipX = TargetPostion.x <= transform.position.x;
	}

	// Update is called once per frame
	private void Update()
	{
		if (hasDoneAction) return;
		//Destroy when out of camera
		if (!sr.isVisible) DestroyBullet();
		
		if (Vector3.Distance(TargetPostion, transform.position) > 0.1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, TargetPostion, movingSpeed * Time.deltaTime);
		}
		else
		{
			transform.position = TargetPostion;
			hasDoneAction = true;
			DestroyBullet();
		}
	}

	public void DestroyBullet()
	{
		Destroy(this.gameObject);
	}
}
