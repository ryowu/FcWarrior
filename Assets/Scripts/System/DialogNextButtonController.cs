using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogNextButtonController : MonoBehaviour
{
	[SerializeField] private Image nextButton;
	[SerializeField] private int flashPeriod = 500;

	DateTime dtStart;
	void Start()
	{
		dtStart = DateTime.Now;
	}

	// Update is called once per frame
	void Update()
	{
		TimeSpan ts = DateTime.Now - dtStart;
		if (ts.TotalMilliseconds > flashPeriod)
		{
			nextButton.enabled = !nextButton.enabled;
			dtStart = DateTime.Now;
		}
	}
}
