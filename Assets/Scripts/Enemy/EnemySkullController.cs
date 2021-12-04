using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullController : EnemyRoutineAction
{
	[SerializeField] private AudioSource skullAttackEffect;
	[SerializeField] private GameObject skullBullet;
	private EnemyLife elife;
	private GameObject skullBulletNew;
	private Renderer render;

	protected override void InnerStart()
	{
		elife = GetComponent<EnemyLife>();
		render = GetComponent<Renderer>();
	}

	protected override void DoRoutineWork()
	{
		if (elife.IsAlive && render.isVisible)
		{
			skullAttackEffect.Play();

			//set bullet
			InitBullet(transform.position.x - 35f, transform.position.y, transform.position.z);
			InitBullet(transform.position.x + 35f, transform.position.y, transform.position.z);
			InitBullet(transform.position.x, transform.position.y + 35f, transform.position.z);
			InitBullet(transform.position.x, transform.position.y - 35f, transform.position.z);
			InitBullet(transform.position.x - 35f, transform.position.y - 35f, transform.position.z);
			InitBullet(transform.position.x + 35f, transform.position.y - 35f, transform.position.z);
			InitBullet(transform.position.x - 35f, transform.position.y + 35f, transform.position.z);
			InitBullet(transform.position.x + 35f, transform.position.y + 35f, transform.position.z);
		}
	}

	private void InitBullet(float x, float y, float z, float speed)
	{
		skullBulletNew = Instantiate(skullBullet, transform.position, transform.rotation, transform.parent);
		SkullBulletController skullBController = skullBulletNew.GetComponent<SkullBulletController>();
		skullBController.TargetPosition = new Vector3(x, y, z);
		skullBController.SkullBulletMovingSpeed = speed;
	}

	private void InitBullet(float x, float y, float z)
	{
		InitBullet(x, y, z, 20f);
	}
}
