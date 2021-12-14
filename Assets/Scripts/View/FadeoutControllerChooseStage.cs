using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeoutControllerChooseStage : MonoBehaviour
{
	[SerializeField] private AudioSource stageSelectEffect;
	[SerializeField] private AudioSource enterStageEffect;
	[SerializeField] private GameObject stageSelectBorder;
	[SerializeField] private Text LoadingLabel;
	[SerializeField] private Text stageName;
	private Animator anim;

	float dirX, dirY;
	float posX, posY;
	int originalStage;
	int stage;
	int maxStage;
	DateTime lastInputTime;

	private bool acceptInput;

	// Start is called before the first frame update
	void Start()
	{
		Cursor.visible = false;
		LoadingLabel.enabled = false;

		stage = 2;
		maxStage = 5;
		lastInputTime = DateTime.Now;

		anim = GetComponent<Animator>();

		acceptInput = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (!acceptInput) return;

		originalStage = stage;
		dirX = Input.GetAxisRaw("Horizontal");
		dirY = Input.GetAxisRaw("Vertical");

		TimeSpan delta = DateTime.Now - lastInputTime;

		if (delta.TotalMilliseconds > 100)
		{
			if (dirX > 0.75f)
				stage++;
			else if (dirX < -0.75f)
				stage--;
			else if (dirY > 0.75f)
				stage--;
			else if (dirY < -0.75)
				stage++;

			if (stage < 2)
				stage = maxStage - 1;
			else if (stage > maxStage - 1)
				stage = 2;

			if (originalStage != stage)
			{
				lastInputTime = DateTime.Now;
				SetStageBorderAndName();
				
			}

			if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.K))
			{
				stageName.enabled = false;
				acceptInput = false;
				enterStageEffect.Play();
				anim.SetTrigger("fadeout"); // this animation event will load the scene
				LoadingLabel.enabled = true;
			}
		}


	}

	private void SetStageBorderAndName()
	{

		switch (stage)
		{
			case 2:
				{
					posX = 0f;
					posY = 0f;
					stageName.text = "静溢森林";
					break;
				}
			case 3:
				{
					posX = 5.74f;
					posY = 0f;
					stageName.text = "云中之城";
					break;
				}
			case 4:
				{
					posX = 11.46f;
					posY = 0f;
					stageName.text = "废弃电厂";
					break;
				}
			case 5:
				{
					posX = 2.74f;
					posY = -4.81f;
					stageName.text = "";
					break;
				}
			case 6:
				{
					posX = 9.29f;
					posY = -4.81f;
					break;
				}
		}

		stageSelectBorder.transform.position = new Vector2(posX, posY);
		stageSelectEffect.Play();

	}

	IEnumerator LoadStageScene()
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GetBuildIndex(stage));

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

	private int GetBuildIndex(int stage)
	{
		int result = 2;
		switch (stage)
		{
			case 2:
				{
					result = 2;
					break;
				}
			case 3:
				{
					result = 8;
					break;
				}
			case 4:
				{
					result = 13;
					break;
				}
		}

		return result;
	}

}
