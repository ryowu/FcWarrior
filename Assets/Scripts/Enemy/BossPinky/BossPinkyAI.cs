using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPinkyAI : MonoBehaviour
{
	[SerializeField] public GameObject Player;
	[SerializeField] private GameObject fireballLittleBullet;
	[SerializeField] private AudioSource fireballLBulletEffect;

	private EnemyBossLife bossLife;
	private EnemyData bossData;
	private SpriteRenderer sr;
	private Animator aim;
	string[] actionList;
	int actionIndex;
	private int bossRageState;
	public bool IsRage
	{
		get
		{
			return bossData.EnemyHP <= bossData.EnemyMaxHP / 2;
		}
	}

	public bool RageShown;

	// Start is called before the first frame update
	void Start()
	{
		aim = GetComponent<Animator>();
		bossLife = GetComponent<EnemyBossLife>();
		sr = GetComponent<SpriteRenderer>();
		bossData = GetComponent<EnemyData>();

		actionList = new string[] { "jumpSide", "jumpSide", "jumpCenter" };
		actionIndex = 0;

		RageShown = false;
	}

	public bool FlipX
	{
		get { return sr.flipX; }
		set { sr.flipX = value; }
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void FaceToPlayer()
	{
		sr.flipX = Player.transform.position.x <= transform.position.x;
	}

	public string GetNextAction()
	{
		string result = actionList[actionIndex];
		actionIndex++;
		if (actionIndex >= actionList.Length)
		{
			actionIndex = 0;
		}
		return result;
	}

	public void FireSprinkle(int level)
	{
		if (level > 3)
		{
			if (!RageShown) return;
		}

		float xTargetPos = 0f;
		if (transform.position.x < 5f)
		{
			xTargetPos = transform.position.x + (level - 1) * 8f;
			sr.flipX = true;
		}
		else
		{
			xTargetPos = transform.position.x - (level - 1) * 8f;
			sr.flipX = false;
		}

		GameObject bulletNew = Instantiate(fireballLittleBullet, transform.position, transform.rotation, transform.parent);
		LittleBulletController lb = bulletNew.GetComponent<LittleBulletController>();
		lb.TargetPostion = new Vector2(xTargetPos, -5f);

		fireballLBulletEffect.Play();
	}

	public void FireCircle(int level)
	{
		if (level > 3)
		{
			if (!RageShown) return;
		}

		GameObject bulletNew;
		LittleBulletController lb;
		switch (level)
		{
			case 1:
				{
					bulletNew = Instantiate(fireballLittleBullet, transform.position, transform.rotation, transform.parent);
					lb = bulletNew.GetComponent<LittleBulletController>();
					lb.TargetPostion = new Vector2(transform.position.x, -5f);
					break;
				}
			case 2:
				{
					bulletNew = Instantiate(fireballLittleBullet, transform.position, transform.rotation, transform.parent);
					lb = bulletNew.GetComponent<LittleBulletController>();
					lb.TargetPostion = new Vector2(transform.position.x - 8f, -5f);

					bulletNew = Instantiate(fireballLittleBullet, transform.position, transform.rotation, transform.parent);
					lb = bulletNew.GetComponent<LittleBulletController>();
					lb.TargetPostion = new Vector2(transform.position.x + 8f, -5f);
					break;
				}
			case 3:
				{
					bulletNew = Instantiate(fireballLittleBullet, transform.position, transform.rotation, transform.parent);
					lb = bulletNew.GetComponent<LittleBulletController>();
					lb.TargetPostion = new Vector2(transform.position.x - 16f, -5f);

					bulletNew = Instantiate(fireballLittleBullet, transform.position, transform.rotation, transform.parent);
					lb = bulletNew.GetComponent<LittleBulletController>();
					lb.TargetPostion = new Vector2(transform.position.x + 16f, -5f);
					break;
				}
			case 4:
				{
					bulletNew = Instantiate(fireballLittleBullet, transform.position, transform.rotation, transform.parent);
					lb = bulletNew.GetComponent<LittleBulletController>();
					lb.TargetPostion = new Vector2(transform.position.x, -5f);

					bulletNew = Instantiate(fireballLittleBullet, transform.position, transform.rotation, transform.parent);
					lb = bulletNew.GetComponent<LittleBulletController>();
					lb.TargetPostion = new Vector2(transform.position.x - 8f, -5f);

					bulletNew = Instantiate(fireballLittleBullet, transform.position, transform.rotation, transform.parent);
					lb = bulletNew.GetComponent<LittleBulletController>();
					lb.TargetPostion = new Vector2(transform.position.x + 8f, -5f);

					bulletNew = Instantiate(fireballLittleBullet, transform.position, transform.rotation, transform.parent);
					lb = bulletNew.GetComponent<LittleBulletController>();
					lb.TargetPostion = new Vector2(transform.position.x - 20f, -5f);

					bulletNew = Instantiate(fireballLittleBullet, transform.position, transform.rotation, transform.parent);
					lb = bulletNew.GetComponent<LittleBulletController>();
					lb.TargetPostion = new Vector2(transform.position.x + 20f, -5f);
					break;
				}
		}
		fireballLBulletEffect.Play();
	}

	public void OnFireSprinkleComplete()
	{
		aim.SetInteger("state", 3);
	}

	public void OnFireCircleComplete()
	{
		aim.SetInteger("state", 3);
	}

	public float GetBossSpeed()
	{
		return bossData.BossSpeed;
	}

	public float GetBossScrollSpeed()
	{
		return bossData.BossSpeed + 12f;
	}

	public void SetImmune(bool isImmune)
	{
		bossLife.IsImmune = isImmune;
	}

	public void OnRageComplete()
	{
		bossData.BossSpeed += 2f;
		//set state to run
		aim.SetInteger("state", 1);
		bossLife.IsImmune = false;
	}
}
