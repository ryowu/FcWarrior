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

	[SerializeField] private GameObject PlayerBullet;
	[SerializeField] private GameObject PlayerSword;
	[SerializeField] private GameObject ChargegunBullet;
	

	[SerializeField] private LayerMask jumpableGround;
	[SerializeField] private LayerMask jumpThroughGround;

	private float dirX, dirY = 0f;

	[SerializeField] private float moveSpeed = 7f;
	[SerializeField] private float jumpForce = 14f;

	private enum MovementState { idle, running, jumping, falling, doubleJump, hit }

	[SerializeField] private AudioSource jumpSoundEffect;
	[SerializeField] private AudioSource shootEffect;
	[SerializeField] private AudioSource swordEffect;
	[SerializeField] private AudioSource chargegunEffect;
	[SerializeField] private AudioSource powerlowEffect;

	DateTime swordStartTime;
	private float swordDistance = 1f;
	[SerializeField] private int swordCooldownTime = 200;

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
		swordStartTime = DateTime.Now;
	}

	// Update is called once per frame
	private void Update()
	{
		if (!GlobalVars.IsPlayerControllable)
		{
			//rb.bodyType = RigidbodyType2D.Dynamic;
			//rb.velocity = new Vector2(0f, 0f);
			//rb.bodyType = RigidbodyType2D.Static;
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

		//Jump through platform ignore
		if (dirY < 0 && (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.K)))
		{
			//if down+jump from a jumpThroughPlatform, then ignore the jump action
			if (IsJumpThroughGrounded())
			{
				return;
			}
		}


		if (IsGrounded() || IsJumpThroughGrounded())
		{
			jumpPhase = 0;
		}

		//cancel jump
		if (Input.GetButtonUp("Fire2") || Input.GetKeyUp(KeyCode.K))
		{
			if (rb.velocity.y > 0f)
				rb.velocity = new Vector2(rb.velocity.x, 0f);
		}

		//Jump
		if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.K))
		{
			if (IsGrounded() || IsJumpThroughGrounded())
			{
				jumpSoundEffect.Play();
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
				jumpPhase = 0;
			}
			else if (jumpPhase == 0 && PlayerData.DoubleJumpEnabled)
			{
				jumpSoundEffect.Play();
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
				jumpPhase++;
			}
		}

		//Sword
		if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.J))
		{
			TimeSpan ts = DateTime.Now - swordStartTime;

			if (ts.TotalMilliseconds > swordCooldownTime)
			{
				//sword distance
				if (PlayerData.PowerSword)
					swordDistance = 2f;

				Vector3 pos;
				if (!sprite.flipX)
					pos = new Vector3(transform.position.x + swordDistance, transform.position.y - 0.13f, transform.position.z);
				else
					pos = new Vector3(transform.position.x - swordDistance, transform.position.y - 0.13f, transform.position.z);

				GameObject swordNew = Instantiate(PlayerSword, pos, transform.rotation, transform);
				SpriteRenderer sr = swordNew.GetComponent<SpriteRenderer>();

				if (PlayerData.PowerSword)
					swordNew.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

				sr.flipX = sprite.flipX;
				swordEffect.Play();

				swordStartTime = DateTime.Now;
			}
		}


		//Bullet
		if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.U))
		{
			GameObject bulletNew = Instantiate(PlayerBullet, transform.position, transform.rotation, transform.parent);
			FireballMoving fb = bulletNew.GetComponent<FireballMoving>();
			fb.TargetPostion = new Vector3(transform.position.x + (sprite.flipX ? -1f : 1f) * 20f, transform.position.y, transform.position.z);

			if (PlayerData.DoubleGun)
			{
				Vector3 newInitPos = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
				bulletNew = Instantiate(PlayerBullet, newInitPos, transform.rotation, transform.parent);
				fb = bulletNew.GetComponent<FireballMoving>();
				fb.TargetPostion = new Vector3(transform.position.x + (sprite.flipX ? -1f : 1f) * 20f, transform.position.y - 0.3f, transform.position.z);
			}

			shootEffect.Play();
		}

		//Side weapon
		if (Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.I))
		{
			if (PlayerData.SideWeaponChargeReady)
			{
				int cost = PlayerData.SideWeaponCostHalf ? 2 : 4;
				if (PlayerData.PlayerDiamond >= cost)
				{
					PlayerData.PlayerDiamond -= cost;
					PlayerData.SideWeaponChargeReady = false;
					GetComponent<ItemCollector>().RefreshDiamondText();

					Vector3 newInitPos = new Vector3(transform.position.x + (sprite.flipX ? -1f : 1f) * 2f, transform.position.y, transform.position.z);
					GameObject bulletChargeGun = Instantiate(ChargegunBullet, newInitPos, transform.rotation, transform.parent);
					FireballMoving fb = bulletChargeGun.GetComponent<FireballMoving>();
					fb.TargetPostion = new Vector3(transform.position.x + (sprite.flipX ? -1f : 1f) * 20f, transform.position.y, transform.position.z);
					chargegunEffect.Play();
				}
				else
				{
					//powerlowEffect.Play();
				}
			}
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
		return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
	}

	private bool IsJumpThroughGrounded()
	{
		return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpThroughGround);
	}

	private bool IsPlayerSqueezed()
	{
		RaycastHit2D reDown = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
		RaycastHit2D reUp = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 180f, Vector2.up, .1f, jumpableGround);
		return reUp.collider != null && reDown.collider != null;
	}
}
