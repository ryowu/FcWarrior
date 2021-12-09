using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeoutControllerChooseStage : MonoBehaviour
{
	[SerializeField] private AudioSource stageSelectEffect;
	[SerializeField] private AudioSource enterStageEffect;
	[SerializeField] private GameObject stageSelectBorder;
	private Animator anim;

	float dirX, dirY;
	float posX, posY;
	int originalStage;
	int stage;
	DateTime lastInputTime;

	private bool acceptInput;

	// Start is called before the first frame update
	void Start()
	{
		Cursor.visible = false;
		stage = 2;
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
				stage = 6;
			else if (stage > 6)
				stage = 2;

			if (originalStage != stage)
			{
				lastInputTime = DateTime.Now;
				SetStageBorder();
			}

			if (Input.GetButtonDown("Fire2"))
			{
				acceptInput = false;
				enterStageEffect.Play();
				anim.SetTrigger("fadeout"); // this animation event will load the scene
			}
		}


	}

	private void SetStageBorder()
	{

		switch (stage)
		{
			case 2:
				{
					posX = 0f;
					posY = 0f;
					break;
				}
			case 3:
				{
					posX = 5.89f;
					posY = 0f;
					break;
				}
			case 4:
				{
					posX = 11.94f;
					posY = 0f;
					break;
				}
			case 5:
				{
					posX = 2.74f;
					posY = -4.81f;
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
		}

		return result;
	}

}
