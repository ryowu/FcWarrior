using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointStation : MonoBehaviour
{
	[SerializeField] private int CheckPointNumber;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (GlobalVars.CheckPointPosition <= CheckPointNumber)
			GlobalVars.CheckPointPosition = CheckPointNumber;
	}
}
