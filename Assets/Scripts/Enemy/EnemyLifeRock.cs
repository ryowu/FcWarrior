using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeRock : EnemyLife
{
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (!PlayerData.RockGunEnabled) return;

		if ((collision.gameObject.CompareTag("Bullet") && !isDieing))
		{
			eData.EnemyHP -= 6;

			if (eData.EnemyHP <= 0)
			{
				isDieing = true;
				enemyCollider.isTrigger = true;

				EnemyDie();
			}
			else
			{
				hitSoundEffect.Play();
			}

			//Destroy bullet
			if (collision.gameObject.CompareTag("Bullet"))
				Destroy(collision.gameObject);
		}
	}
}
