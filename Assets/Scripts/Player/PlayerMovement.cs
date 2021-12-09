using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D rb;
	private BoxCollider2D coll;
	private SpriteRenderer sprite;
	private Animator anim;

	private int jumpPhase;
	MovementState state;

	[SerializeField] private GameObject bulletPlayer;

	[SerializeField] private LayerMask jumpableGround;
	[SerializeField] private LayerMask jumpThroughGround;

	private float dirX, dirY = 0f;

	[SerializeField] private float moveSpeed = 7f;
	[SerializeField] private float jumpForce = 14f;
	//[SerializeField] private int bulletGapTime = 500;
	//private DateTime bulletStartShootTime;
	//[SerializeField] private int bulletMaxCount = 3;
	//private int bulletCurrentCount;
	[SerializeField] private float bulletSpeed = 20f;

	private enum MovementState { idle, running, jumping, falling, doubleJump, hit }

	[SerializeField] private AudioSource jumpSoundEffect;
	[SerializeField] private AudioSource shootEffect;

	// Start is called before the first frame update
	private void Start()
	{
		GlobalVars.IsCameraFollowing = true;
		GlobalVars.IsPlayerControllable = true;

		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D>();
		sprite = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		jumpPhase = 0;

		//bulletStartShootTime = DateTime.Now;
		//bulletCurrentCount = 0;
	}

	// Update is called once per frame
	private void Update()
	{
		if (!GlobalVars.IsPlayerControllable)
		{
			rb.bodyType = RigidbodyType2D.Dynamic;
			rb.velocity = new Vector2(0f, 0f);
			rb.bodyType = RigidbodyType2D.Static;
			return;
		}
		else
		{
			rb.bodyType = RigidbodyType2D.Dynamic;
		}

		if (rb.bodyType == RigidbodyType2D.Static) return;

		dirX = Input.GetAxisRaw("Horizontal");
		rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

		dirY = Input.GetAxisRaw("Vertical");

		if (dirY < 0 && Input.GetButtonDown("Fire2"))
		{
			//if down+jump from a jumpThroughPlatform, then ignore the jump action
			if (IsJumpThroughGrounded())
			{
				//Debug.Log("down+jump in player action");
				return;
			}
		}

		if (Input.GetButtonDown("Fire2") && (IsGrounded() || jumpPhase == 1 || IsJumpThroughGrounded()))
		{
			jumpSoundEffect.Play();
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);

			if (jumpPhase == 0)
			{
				jumpPhase = 1;
			}
			else if (jumpPhase == 1)
			{
				jumpPhase = 2;
			}
		}

		if (Input.GetButtonDown("Fire1"))
		{
			//if (bulletCurrentCount >= bulletMaxCount)
			//{
			//	TimeSpan ts = DateTime.Now - bulletStartShootTime;
			//	//cooldown finish
			//	if (ts.TotalMilliseconds >= bulletGapTime)
			//	{
			//		bulletCurrentCount = 0;
			//	}
			//}
			//else
			//{
			//	bulletCurrentCount++;

			//	bulletStartShootTime = DateTime.Now; // remember the last shoot time

			//	GameObject bulletNew = Instantiate(bulletPlayer, transform.position, transform.rotation, transform.parent);
			//	FireballMoving fb = bulletNew.GetComponent<FireballMoving>();
			//	fb.TargetPostion = new Vector3(transform.position.x + (sprite.flipX ? -1f : 1f) * 20f, transform.position.y, transform.position.z);
			//	fb.MovingSpeed = bulletSpeed;
			//	shootEffect.Play();
			//}

			GameObject bulletNew = Instantiate(bulletPlayer, transform.position, transform.rotation, transform.parent);
			FireballMoving fb = bulletNew.GetComponent<FireballMoving>();
			fb.TargetPostion = new Vector3(transform.position.x + (sprite.flipX ? -1f : 1f) * 20f, transform.position.y, transform.position.z);
			fb.MovingSpeed = bulletSpeed;
			shootEffect.Play();
		}

		if (Input.GetButtonDown("Fire3"))
		{
			//coll.isTrigger = true;
		}

		UpdateAnimationState();
	}

	private void UpdateAnimationState()
	{

		if (dirX > 0f)
		{
			state = MovementState.running;
			sprite.flipX = false;
		}
		else if (dirX < 0f)
		{
			state = MovementState.running;
			sprite.flipX = true;
		}
		else
		{
			state = MovementState.idle;
		}

		if (rb.velocity.y > .1f)
		{
			state = MovementState.jumping;
		}
		else if (rb.velocity.y < -.1f)
		{
			state = MovementState.falling;
		}

		anim.SetInteger("state", (int)state);
	}

	private bool IsGrounded()
	{
		bool result = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
		if (result)
		{
			jumpPhase = 0;
		}
		return result;
	}

	private bool IsJumpThroughGrounded()
	{
		bool result = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpThroughGround);
		if (result)
		{
			jumpPhase = 0;
		}
		return result;
	}
}
