using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
	[SerializeField] private Image DialogNextButton;
	[SerializeField] private Text DialogTextWindow;
	[SerializeField] private string[] DialogTextContent;
	private int textIndex;
	void Start()
	{
		DialogNextButton.enabled = false;
		textIndex = 0;
		DialogTextWindow.text = DialogTextContent[textIndex];
	}

	// Update is called once per frame
	void Update()
	{
		if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.J)) && this.gameObject.activeSelf)
		{
			textIndex++;
			if (textIndex <= DialogTextContent.Length - 1)
			{
				DialogTextWindow.text = DialogTextContent[textIndex];
				DialogNextButton.enabled = true;
			}
			else
			{
				this.gameObject.SetActive(false);
				GlobalVars.IsPlayerControllable = true;
			}
		}
	}
}
