using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMoving : MonoBehaviour
{
	public Vector3 TargetPostion;
	public float MovingSpeed;
	private Renderer render;

	void Start()
	{
		render = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update()
	{
		//Destroy when out of camera
		if (!render.isVisible) Destroy(this.gameObject);

		if (Vector3.Distance(TargetPostion, transform.position) > 0.1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, TargetPostion, MovingSpeed * Time.deltaTime);
		}
		else
		{
			transform.position = TargetPostion;
			Destroy(this.gameObject);
		}
	}

	//void OnTriggerEnter2D(Collider2D collision)
	//{
	//	if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Cherry" || collision.gameObject.layer == 6)
	//	{
	//		Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	//	}
	//	else
	//	{
	//		Destroy(this.gameObject);
	//	}
	//}

}
