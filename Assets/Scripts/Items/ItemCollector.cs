using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
	private int coinCount;

	[SerializeField] private AudioSource collectSoundEffect;
	[SerializeField] Text itemText;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("dropItem"))
		{
			//Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			coinCount += GetItemValue(collision.gameObject.name);

			collectSoundEffect.Play();

			Destroy(collision.gameObject);
			itemText.text = string.Format("{0}", coinCount);
		}
	}

	private int GetItemValue(string itemName)
	{
		int valueResult = 1;
		if (itemName.IndexOf("Diamond") > -1)
			valueResult = 10;
		else if (itemName.IndexOf("Coin") > -1)
			valueResult = 1;
		return valueResult;
	}
}
