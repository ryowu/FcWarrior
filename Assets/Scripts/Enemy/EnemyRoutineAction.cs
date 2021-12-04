using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoutineAction : MonoBehaviour
{
	[SerializeField] private float routineTimePeriod = 2000f;

	DateTime startRoutineTime;
	TimeSpan ts;

	// Start is called before the first frame update
	void Start()
	{
		startRoutineTime = DateTime.Now;
		InnerStart();
	}

	// Update is called once per frame
	void Update()
	{
		ts = DateTime.Now - startRoutineTime;
		if (ts.TotalMilliseconds > routineTimePeriod)
		{
			DoRoutineWork();
			startRoutineTime = DateTime.Now;
		}

		InnerUpdate();
	}

	protected virtual void DoRoutineWork() { }

	protected virtual void InnerStart() { }

	protected virtual void InnerUpdate() { }
}
