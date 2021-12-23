using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAboriginalAI : MonoBehaviour
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

		actionList = new string[] { "scroll", "smash" };
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

	public float GetBossScrollSpeed()
	{
		return bossData.BossSpeed + 12f;
	}

	public void SetImmune(bool isImmune)
	{
		bossLife.IsImmune = isImmune;
	}

	public void FireLava()
	{
		FaceToPlayer();

		GameObject bulletNew = Instantiate(fireballRound, transform.position, transform.rotation, transform.parent);
		BossFireballRound fh = bulletNew.GetComponent<BossFireballRound>();
		fh.TargetPostion = new Vector2(transform.transform.position.x - 30f, transform.position.y);
		fh.MovingSpeed = 25f;

		bulletNew = Instantiate(fireballRound, transform.position, transform.rotation, transform.parent);
		BossFireballRound fh1 = bulletNew.GetComponent<BossFireballRound>();
		fh1.TargetPostion = new Vector2(transform.transform.position.x + 30f, transform.position.y);
		fh1.MovingSpeed = 25f;

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
