using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSelfAI : MonoBehaviour
{
	[SerializeField] public GameObject Player;
	[SerializeField] private GameObject selfBullet;
	[SerializeField] private AudioSource bulletEffect;
	[SerializeField] private GameObject FinalDialog;
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

		actionList = new string[] { "Shoot", "Rush", "JumpShoot" };
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

	public void OnShootNormal()
	{
		FaceToPlayer();
		//boss shoot one horizon fireball
		GameObject bulletNew = Instantiate(selfBullet, transform.position, transform.rotation, transform.parent);
		Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), bulletNew.GetComponent<BoxCollider2D>());
		bulletNew.GetComponent<SpriteRenderer>().flipX = !sr.flipX;
		BossSelfBulletController fh = bulletNew.GetComponent<BossSelfBulletController>();
		fh.TargetPostion = new Vector3(transform.position.x + (sr.flipX ? -1f : 1f) * 20f, transform.position.y, transform.position.z);
		fh.MovingSpeed = 20f;
		bulletEffect.Play();
	}

	public void OnShootNormalComplete()
	{
		aim.SetInteger("state", 1);
	}

	public void OnFireCircle(int level)
	{
		float delta = -10f;
		if (transform.position.x < -1f)
			delta = delta * -1f;

		switch (level)
		{
			case 1:
				{
					InitBullet(new Vector2(transform.position.x, -5f));
					InitBullet(new Vector2(transform.position.x + delta, -5f));
					InitBullet(new Vector2(transform.position.x + delta * 2f, -5f));
					break;
				}
			case 2:
				{
					InitBullet(new Vector2(transform.position.x + delta / 2f, -5f));
					InitBullet(new Vector2(transform.position.x + delta * 1.5f, -5f));
					break;
				}
			case 3:
				{
					InitBullet(new Vector2(transform.position.x, -5f));
					InitBullet(new Vector2(transform.position.x + delta, -5f));
					InitBullet(new Vector2(transform.position.x + delta * 2f, -5f));
					break;
				}
			case 4:
				{
					InitBullet(new Vector2(Player.transform.position.x, Player.transform.position.y));
					break;
				}
		}
		bulletEffect.Play();
	}

	private void InitBullet(Vector2 target)
	{
		GameObject bulletNew;
		BossSelfBulletController lb;
		bulletNew = Instantiate(selfBullet, transform.position, transform.rotation, transform.parent);
		Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), bulletNew.GetComponent<BoxCollider2D>());
		lb = bulletNew.GetComponent<BossSelfBulletController>();
		lb.TargetPostion = target;
		lb.MovingSpeed = 20f;
	}

	public void OnFireCircleComplete()
	{
		//set to fall
		aim.SetInteger("state", 6);
	}

	public float GetBossSpeed()
	{
		return bossData.BossSpeed;
	}

	public float GetBossDashSpeed()
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

	public void ShowFinalDialog()
	{
		FinalDialog.SetActive(true);
	}
}
