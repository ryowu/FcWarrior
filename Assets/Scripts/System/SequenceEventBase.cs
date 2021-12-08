using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceEventBase : MonoBehaviour
{
	public bool IsStarted { get { return isStart; } }

	protected bool isStart;

	public bool EventComplete { get { return _eventComplete; } }

	protected bool _eventComplete;

	public int WaitTime;
	public bool StartWait;

	public void StartEvent()
	{
		isStart = true;
	}
}
