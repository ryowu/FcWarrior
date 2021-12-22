using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCGameCardConroller : MonoBehaviour
{
	[SerializeField] private GameObject dialog;
	[SerializeField] private AudioSource skillEffect;
	[SerializeField] private int SkillIndex;

	private void Start()
	{
		//if skill is aquired, destroy this
		switch (SkillIndex)
		{
			case 1://double gun
				{
					if (PlayerData.DoubleGun)
						Destroy(this.gameObject);
					break;
				}
			case 2://double defence
				{
					if (PlayerData.DoubleDef)
						Destroy(this.gameObject);
					break;
				}
			case 3://recover powerup
				{
					if (PlayerData.RecoverPowerUp)
						Destroy(this.gameObject);
					break;
				}
			case 4://side weapon cost half
				{
					if (PlayerData.SideWeaponCostHalf)
						Destroy(this.gameObject);
					break;
				}
			case 5://side weapon cool down half
				{
					if (PlayerData.SideWeaponCooldownHalf)
						Destroy(this.gameObject);
					break;
				}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			GlobalVars.IsPlayerControllable = false;
			skillEffect.Play();
			GetComponent<Animator>().SetTrigger("collect");
			dialog.SetActive(true);

			//get new skill
			switch (SkillIndex)
			{
				case 1://double gun
					{
						PlayerData.DoubleGun = true;
						break;
					}
				case 2://double defence
					{
						PlayerData.DoubleDef = true;
						break;
					}
				case 3://recover powerup
					{
						PlayerData.RecoverPowerUp = true;
						break;
					}
				case 4://side weapon cost half
					{
						PlayerData.SideWeaponCostHalf = true;
						break;
					}
				case 5://side weapon cool down half
					{
						PlayerData.SideWeaponCooldownHalf = true;
						PlayerData.SideWeaponCooldownPeriod = 600;
						break;
					}
			}
		}
	}

	public void OnItemCollected()
	{
		Destroy(this.gameObject);
	}
}
