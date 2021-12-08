using System;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBase : MonoBehaviour
{
	[SerializeField] GameObject[] wayPoints;
	private int currentWaypointIndex = 0;
	[SerializeField] float movingSpeed = 2f;

	protected int WaitTime;
	private DateTime startWaitTime;
	protected bool startWait = false;

	// Update is called once per frame
	void Update()
	{
		if (!BeforePatroling()) return;

		if (startWait)
		{
			TimeSpan ts = DateTime.Now - startWaitTime;
			if (ts.TotalMilliseconds > WaitTime)
			{
				startWait = false;
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

		if (Vector2.Distance(wayPoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
		{
			OnPatrolPointArrived(wayPoints[currentWaypointIndex].transform.position);

			currentWaypointIndex++;

			if (currentWaypointIndex >= wayPoints.Length)
				currentWaypointIndex = 0;
		}

		transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWaypointIndex].transform.position, movingSpeed * Time.deltaTime);
		OnPatroling(transform.position, wayPoints[currentWaypointIndex].transform.position);
	}

	protected virtual void OnPatroling(Vector3 vFrom, Vector3 vTo)
	{

	}

	protected virtual bool BeforePatroling()
	{
		return true;
	}

	protected virtual void OnPatrolPointArrived(Vector3 pos)
	{

	}
}
