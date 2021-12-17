using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutController : MonoBehaviour
{
	[SerializeField] private int StageSceneIndex;
	[SerializeField] private bool UseFireButtonToTriggerFadeout = false;
	private bool hasInputSth;

	private void Start()
	{
		hasInputSth = false;
	}

	private void Update()
	{
		if (hasInputSth) return;
		if (Input.anyKeyDown && UseFireButtonToTriggerFadeout)
		{
			hasInputSth = true;
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
