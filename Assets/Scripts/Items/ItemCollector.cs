using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
	private int coinCount;

	[SerializeField] private AudioSource collectSoundEffect;
	[SerializeField] private AudioSource recoverSoundEffect;
	[SerializeField] Text itemText;
	[SerializeField] private HealthyBarController healthyBar;

	[SerializeField] private GameObject recoverEffect;

	private void Start()
	{
		coinCount = PlayerData.PlayerDiamond;
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

				PlayerData.PlayerHP += 2;
				healthyBar.SetHPValue(PlayerData.PlayerHP);
			}
			else
			{
				//Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
				coinCount += GetItemValue(collision.gameObject.name);

				//save to global
				PlayerData.PlayerDiamond = coinCount;

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

	private void RefreshDiamondText()
	{
		if (itemText != null)
			itemText.text = string.Format("{0}", coinCount);
	}
}
