using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
	class EnemyTrunkController : EnemyRoutineAction
	{
		[SerializeField] private AudioSource trunkAttackEffect;
		[SerializeField] private GameObject trunkBullet;

		private EnemyLife elife;
		private Animator aim;
		private Renderer render;

		GameObject trunkBulletNew;

		protected override void InnerStart()
		{
			aim = GetComponent<Animator>();
			elife = GetComponent<EnemyLife>();
			render = GetComponent<Renderer>();
		}

		protected override void DoRoutineWork()
		{
			if (elife.IsAlive && render.isVisible)
			{
				aim.SetInteger("state", 1);
				aim.SetTrigger("attack");
			}
		}

		private void OnAttackComplete()
		{
			if (elife.IsAlive)
			{
				trunkAttackEffect.Play();
				aim.SetInteger("state", 0);

				//set bullet
				trunkBulletNew = Instantiate(trunkBullet, transform.position, transform.rotation, transform.parent);
				trunkBulletNew.transform.position = new Vector3(transform.position.x - 1f, transform.position.y - 0.2f, transform.position.z);
			}
		}
	}
}
