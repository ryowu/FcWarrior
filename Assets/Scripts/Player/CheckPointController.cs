using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointController : MonoBehaviour
{
	private GameObject Player;
	private GameObject mCam;
	private CameraController cameraController;

	// Start is called before the first frame update
	void Start()
	{
		//SceneManager.sceneLoaded += SceneManager_sceneLoaded;
	}

	private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		mCam = GameObject.FindGameObjectWithTag("MainCamera");
		cameraController = mCam.GetComponent<CameraController>();

		GlobalVars.IsCameraFollowing = true;


		if (arg0.buildIndex == 1)
		{
			switch (GlobalVars.CheckPointPosition)
			{
				case 1:
					{
						Player.transform.position = new Vector3(-11.04f, -0.97f, 0f);
						cameraController.boundXLeft = 5.56f;
						cameraController.boundXRight = 183.57f;
						cameraController.boundYTop = 44.1f;
						cameraController.boundYBottom = 5.48f;
						mCam.transform.position = new Vector3(5.56f, 5.48f, -10f);
						break;
					}
				case 2:
					{
						Player.transform.position = new Vector3(190.93f, -0.97f, 0f);
						cameraController.boundXLeft = 5.56f;
						cameraController.boundXRight = 183.57f;
						cameraController.boundYTop = 44.1f;
						cameraController.boundYBottom = 5.48f;
						mCam.transform.position = new Vector3(183.57f, 5.48f, -10f);
						break;
					}
				case 3:
					{
						Player.transform.position = new Vector3(204.32f, -67.96f, 0f);
						cameraController.boundXLeft = 220.45f;
						cameraController.boundXRight = 278.89f;
						cameraController.boundYTop = -60.21f;
						cameraController.boundYBottom = -60.21f;
						mCam.transform.position = new Vector3(220.45f, -60.21f, -10f);
						
						break;
					}
				case 0:
					{
						break;
					}
			}
		}
	}
}
