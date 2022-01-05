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
	[SerializeField] private Text stageDescription;

	[SerializeField] private Camera cameraMain;
	[SerializeField] private GameObject stage1;
	[SerializeField] private GameObject stage2;
	[SerializeField] private GameObject stage3;
	[SerializeField] private GameObject stage4;

	[SerializeField] private GameObject skillGroup;
	[SerializeField] private GameObject skillBox;
	private Animator anim;

	float dirX, dirY;
	float posX, posY;
	int originalStage;
	int stage;
	int maxStage;
	DateTime lastInputTime;

	private bool acceptInput;


	private string password;


	// Start is called before the first frame update
	void Start()
	{
		Cursor.visible = false;
		LoadingLabel.enabled = false;
		password = string.Empty;

		stage = 2;

		//show final boss stage
		if (PlayerData.DoubleJumpEnabled && PlayerData.PowerSword && PlayerData.RockGunEnabled)
		{
			stage4.SetActive(true);
			maxStage = 6;
		}
		else
		{
			stage4.SetActive(false);
			maxStage = 5;
		}


		lastInputTime = DateTime.Now;

		anim = GetComponent<Animator>();

		acceptInput = true;
	}

	// Update is called once per frame
	void Update()
	{
		password += Input.inputString;
		if (password.Length > 8)
		{
			char firstChar = password[0];
			password = password.Substring(1) + Input.inputString;
		}

		for (int i = password.Length - 1; i >= 0; i--)
		{
			if (i > 0)
			{
				if (password[i] == password[i - 1])
				{
					password = password.Remove(i, 1);
				}
			}
		}

		if (password.IndexOf("hpmax") > -1)
		{
			password = string.Empty;
			enterStageEffect.Play();
			//HP 1000
			PlayerData.HP1000 = true;
			stageName.text = "Ѫ��1000����";
			stageDescription.text = "�����������1000��Ѫ��Ӧ����ͨ�ء������������˰ɣ�";
		}

		if (password.IndexOf("life30") > -1)
		{
			password = string.Empty;
			enterStageEffect.Play();
			//HP 1000
			PlayerData.PlayerLife = 30;
			stageName.text = "����30����";
			stageDescription.text = "��Ȼ���Ⲣ�����������ƨ�ĵط���������";
		}

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

			//set skill group position
			skillGroup.transform.position = cameraMain.WorldToScreenPoint(skillBox.transform.position + new Vector3(-6.5f, -1.8f, 0f));
		}


	}

	private void SetStageBorderAndName()
	{
		Vector2 stagePos = stage1.transform.position;
		switch (stage)
		{
			case 2:
				{
					stagePos = stage1.transform.position;
					stageName.text = "����ɭ��";
					stageDescription.text = "��Ƭ����������͵�ɭ��ʵ�����Ѿ�����Ⱦ�����������Ұ���Ѿ������˷����κ������ߵ�׼��";
					break;
				}
			case 3:
				{
					stagePos = stage2.transform.position;
					stageName.text = "����֮��";
					stageDescription.text = "�������Ƶĳ����ż��г����˸������壬��������ÿһ������Ծ���ɳ������ŵ�ð����";
					break;
				}
			case 4:
				{
					stagePos = stage3.transform.position;
					stageName.text = "�����糧";
					stageDescription.text = "�ķ�����õķ��糧������ռ�ݣ������ƺ�ĳЩ�豸������ת����С�Ĳ�Ҫ����";
					break;
				}
			case 5:
				{
					stagePos = stage4.transform.position;
					stageName.text = "�þ�����";
					stageDescription.text = "�ڵ�ƽ�ߵ��Ǳ�ͻȻ������һ�����ݣ����ʵ�שǽ�ͱ���Ļ��ش����˲����Ԥ��";
					break;
				}
		}

		stageSelectBorder.transform.position = new Vector2(stagePos.x + 5.13f, stagePos.y - 2.57f);
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
			case 5:
				{
					result = 20;
					break;
				}
		}

		return result;
	}

}
