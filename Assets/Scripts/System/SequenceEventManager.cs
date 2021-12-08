using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceEventManager : MonoBehaviour
{
	[SerializeField] private GameObject[] SequenceGameObjects;

	private int objectIndex;

	//timeout second
	private int timeoutPeriod;
	private DateTime timeoutStartedTime;

	SequenceEventBase sbase;

	private DateTime startWaitTime;

	void Start()
	{
		objectIndex = 0;
		timeoutPeriod = 60;
	}

	// Update is called once per frame
	void Update()
	{
		//Done all events, then return
		if (objectIndex >= SequenceGameObjects.Length) return;

		//get current event and start it, also record the start time
		sbase = SequenceGameObjects[objectIndex].GetComponent<SequenceEventBase>();
		if (!sbase.IsStarted)
		{
			sbase.StartEvent();
			timeoutStartedTime = DateTime.Now;
		}

		//Wait 
		if (sbase.StartWait)
		{
			TimeSpan tsWait = DateTime.Now - startWaitTime;
			if (tsWait.TotalMilliseconds > sbase.WaitTime)
			{
				sbase.StartWait = false;
			}
			else
			{
				return;
			}
		}
		else
		{
			startWaitTime = DateTime.Now;
		}

		//calculate the timeout period
		TimeSpan tsTimeout = DateTime.Now - timeoutStartedTime;

		//if event done of timeout, move to next event
		if (sbase.EventComplete || tsTimeout.TotalSeconds > timeoutPeriod)
		{
			objectIndex++;
		}
	}
}
