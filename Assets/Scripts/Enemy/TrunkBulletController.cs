using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkBulletController : MonoBehaviour
{
	[SerializeField] private float trunkBulletMovingSpeed = 12f;
	private Vector3 targetBulletPostion;
	private SpriteRenderer sr;
	private float targetDistance;

	// Start is called before the first frame update
	void Start()
	{

		sr = GetComponent<SpriteRenderer>();
		targetDistance = 35f;
		targetDistance *= sr.flipX ? 1 : -1;

		targetBulletPostion = new Vector3(transform.position.x + targetDistance, transform.position.y, transform.position.z);
	}

	// Update is called once per frame
	void Update()
	{
		if (Vector3.Distance(targetBulletPostion, transform.position) > 0.1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetBulletPostion, trunkBulletMovingSpeed * Time.deltaTime);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
