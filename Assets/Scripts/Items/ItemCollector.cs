using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
	[SerializeField] private AudioSource collectSoundEffect;
	[SerializeField] private AudioSource recoverSoundEffect;
	[SerializeField] Text itemText;
	[SerializeField] private HealthyBarController healthyBar;

	[SerializeField] private GameObject recoverEffect;

	private void Start()
	{
		RefreshDiamondText();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("dropItem"))
		{
			//hp recover
			if (collision.gameObject.name.IndexOf("Cherry") > -1)
			{
				recoverSoundEffect.Play();

				GameObject recoverHp = Instantiate(recoverEffect, transform.position, transform.rotation, transform);

				int restoreHPvalue = 2;
				if (PlayerData.RecoverPowerUp) restoreHPvalue = (int)(restoreHPvalue * 1.5);
				PlayerData.PlayerHP += restoreHPvalue;
				healthyBar.SetHPValue(PlayerData.PlayerHP);
			}
			else
			{
				//Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
				PlayerData.PlayerDiamond += GetItemValue(collision.gameObject.name);

				collectSoundEffect.Play();
				RefreshDiamondText();
			}

			Destroy(collision.gameObject);

		}
	}

	private int GetItemValue(string itemName)
	{
		int valueResult = 0;
		if (itemName.IndexOf("Diamond") > -1)
			valueResult = 10;
		else if (itemName.IndexOf("Coin") > -1)
			valueResult = 1;
		return valueResult;
	}

	public void RefreshDiamondText()
	{
		if (itemText != null)
			itemText.text = string.Format("{0}", PlayerData.PlayerDiamond);
	}
}
