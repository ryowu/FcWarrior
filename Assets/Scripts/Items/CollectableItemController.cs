using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemController : MonoBehaviour
{
	private Vector3 targetPos;

	private BoxCollider2D coll;
	private Rigidbody2D rigidBody;
	private SpriteRenderer sprite;
	[SerializeField] private LayerMask jumpableGround;
	[SerializeField] private float movingSpeed = 8f;
	[SerializeField] private bool CanDisappear = true;

	private bool isOnGround;
	private DateTime createdTime;
	private DateTime flashTime;
	// Start is called before the first frame update
	void Start()
	{
		isOnGround = false;
		createdTime = DateTime.Now;
		flashTime = DateTime.Now;
		coll = GetComponent<BoxCollider2D>();
		//rigidBody = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		targetPos = new Vector3(transform.position.x, -9999f, 0f);
	}

	// Update is called once per frame
	void Update()
	{
		if (!isOnGround)
		{
			if (IsGrounded())
			{
				isOnGround = true;
				//rigidBody.bodyType = RigidbodyType2D.Static;
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, targetPos, movingSpeed * Time.deltaTime);
			}
		}
		else if (CanDisappear)
		{
			TimeSpan ts = DateTime.Now - createdTime;
			if (ts.TotalSeconds > 3f)
			{
				TimeSpan tsFlash = DateTime.Now - flashTime;
				if (tsFlash.TotalMilliseconds > 80f)
				{
					if (sprite.color.a < 1f)
					{
						sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
					}
					else
					{
						sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.1f);
					}
					flashTime = DateTime.Now;
				}
			}

			if (ts.TotalSeconds > 5f)
			{
				Destroy(this.gameObject);
			}
		}
	}

	private bool IsGrounded()
	{
		return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
	}
}
