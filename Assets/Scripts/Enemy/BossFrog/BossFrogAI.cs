using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFrogAI : MonoBehaviour
{
	[SerializeField] public GameObject Player;
	[SerializeField] private GameObject fireballHorizon;
	[SerializeField] private GameObject fireballRound;
	[SerializeField] private AudioSource fireballHorizonEffect;
	[SerializeField] private AudioSource fireballLavaEffect;
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

		actionList = new string[] { "fireHorizon", "fireHorizon", "fireLava" };
		actionIndex = 0;

		RageShown = false;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void FaceToPlayer()
	{
		sr.flipX = Player.transform.position.x <= transform.position.x;
	}

	public string GetFireAction()
	{
		string result = actionList[actionIndex];
		actionIndex++;
		if (actionIndex >= actionList.Length)
		{
			actionIndex = 0;
		}
		return result;
	}

	public float GetBossSpeed()
	{
		return bossData.BossSpeed;
	}

	public void SetImmune(bool isImmune)
	{
		bossLife.IsImmune = isImmune;
	}

	public void FireHorizon()
	{
		FaceToPlayer();
		//boss shoot one horizon fireball
		GameObject bulletNew = Instantiate(fireballHorizon, transform.position, transform.rotation, transform.parent);
		bulletNew.GetComponent<SpriteRenderer>().flipX = !sr.flipX;
		BossFireballHorizon fh = bulletNew.GetComponent<BossFireballHorizon>();
		fh.TargetPostion = new Vector3(transform.position.x + (sr.flipX ? -1f : 1f) * 20f, transform.position.y, transform.position.z);
		fh.MovingSpeed = 20f;
		fireballHorizonEffect.Play();

		if (!RageShown)
		{
			//set state to run
			aim.SetInteger("state", 1);
		}
	}

	public void FireHorizonSecond()
	{
		if (RageShown)
		{
			FireHorizon();
			//set state to run
			aim.SetInteger("state", 1);
		}
	}

	public void FireLava(int level)
	{
		FaceToPlayer();

		if (level == 1)
		{
			GameObject bulletNew = Instantiate(fireballRound, transform.position, transform.rotation, transform.parent);
			BossFireballRound fh = bulletNew.GetComponent<BossFireballRound>();
			fh.TargetPostion = new Vector2(Player.transform.position.x, Player.transform.position.y + 8f);
			fh.MovingSpeed = 25f;

			bulletNew = Instantiate(fireballRound, transform.position, transform.rotation, transform.parent);
			BossFireballRound fh1 = bulletNew.GetComponent<BossFireballRound>();
			fh1.TargetPostion = new Vector2(Player.transform.position.x - 4f, Player.transform.position.y + 8f);
			fh1.MovingSpeed = 25f;

			bulletNew = Instantiate(fireballRound, transform.position, transform.rotation, transform.parent);
			BossFireballRound fh2 = bulletNew.GetComponent<BossFireballRound>();
			fh2.TargetPostion = new Vector2(Player.transform.position.x + 4f, Player.transform.position.y + 8f);
			fh2.MovingSpeed = 25f;
		}
		else if (level == 2)
		{
			if (!RageShown) return;
			GameObject bulletNew = Instantiate(fireballRound, transform.position, transform.rotation, transform.parent);
			BossFireballRound fh1 = bulletNew.GetComponent<BossFireballRound>();
			fh1.TargetPostion = new Vector2(Player.transform.position.x - 2f, Player.transform.position.y + 8.5f);
			fh1.MovingSpeed = 25f;

			bulletNew = Instantiate(fireballRound, transform.position, transform.rotation, transform.parent);
			BossFireballRound fh2 = bulletNew.GetComponent<BossFireballRound>();
			fh2.TargetPostion = new Vector2(Player.transform.position.x + 2f, Player.transform.position.y + 8.5f);
			fh2.MovingSpeed = 25f;
		}

		fireballLavaEffect.Play();
	}

	public void OnFireLavaComplete()
	{
		//set state to run
		aim.SetInteger("state", 1);
	}

	public void OnRageComplete()
	{
		bossData.BossSpeed += 2f;
		//set state to run
		aim.SetInteger("state", 1);
		bossLife.IsImmune = false;
	}
}
