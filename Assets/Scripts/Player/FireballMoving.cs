using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMoving : MonoBehaviour
{
	// Start is called before the first frame update
	private float startPositionX;

	void Start()
	{
		startPositionX = transform.position.x;
	}

	// Update is called once per frame
	void Update()
	{
		if (Mathf.Abs(startPositionX - transform.position.x) > 20f)
		{
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
