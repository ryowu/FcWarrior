using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSquareManager : MonoBehaviour
{
	[SerializeField] private GameObject[] Squares;
	[SerializeField] private int Interval = 1000;

	private DateTime startTime;
	private int squareIndex = 0;
	private int squareIndexLast;

	// Start is called before the first frame update
	void Start()
	{
		startTime = DateTime.Now;

		if (Squares.Length > 0)
			squareIndexLast = Squares.Length - 1;

		DisableAllSquares();
	}

	private void DisableAllSquares()
	{
		foreach (GameObject go in Squares)
		{
			go.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update()
	{
		TimeSpan ts = DateTime.Now - startTime;
		if (ts.TotalMilliseconds > Interval)
		{
			//Disable the last one
			Squares[squareIndexLast].SetActive(false);
			//Enable the current one
			Squares[squareIndex].SetActive(true);
			//Remember last index
			squareIndexLast = squareIndex;
			//Move index to next
			squareIndex++;
			//If reach end, reset from the first one
			if (squareIndex >= Squares.Length)
				squareIndex = 0;
			startTime = DateTime.Now;
		}
	}
}
