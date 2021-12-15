using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutController : MonoBehaviour
{
	[SerializeField] private int StageSceneIndex;
	[SerializeField] private bool UseFireButtonToTriggerFadeout = false;


	private void Update()
	{
		if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.J)) && UseFireButtonToTriggerFadeout)
		{
			GetComponent<Animator>().SetTrigger("fadeout");
		}
	}

	IEnumerator LoadStageScene()
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(StageSceneIndex);

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}

	public void OnFadeOutComplete()
	{
		StartCoroutine(LoadStageScene());
	}
}
