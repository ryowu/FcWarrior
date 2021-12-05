using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutController : MonoBehaviour
{
	[SerializeField] private int StageSceneIndex;

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
