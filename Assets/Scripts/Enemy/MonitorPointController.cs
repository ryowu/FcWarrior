using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorPointController : MonoBehaviour
{
	[SerializeField] private GameObject monitorHost;
	private EnemyMovement enemyMovmentObject;


	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && monitorHost != null)
		{
			enemyMovmentObject = monitorHost.GetComponent<EnemyMovement>();
			enemyMovmentObject.MonitorTargetPostion = collision.transform.position;
			enemyMovmentObject.IsReturning = false;
		}
	}
}
