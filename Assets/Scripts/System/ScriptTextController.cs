using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTextController : MonoBehaviour
{
	[SerializeField] private Text scriptText;
	[SerializeField] private Image scriptImage;
	[SerializeField] private int showTextGap = 100;
	[SerializeField] private int showSentenceGap = 2000;
	[SerializeField] private GameObject fadeinoutImage;
	[SerializeField] private AudioSource bgm_Start;
	[SerializeField] private AudioSource bgm_nervous;

	[SerializeField] private Sprite[] storyPics;

	string[] scripts;
	int scriptIndex;
	int charIndex;
	bool showTextComplete;
	bool inSentenceGap;
	int changeGBM_Index;
	DateTime dtCharStart;
	DateTime dtSentenceStart;

	// Start is called before the first frame update
	void Start()
	{
		scriptIndex = 0;
		charIndex = 1;
		inSentenceGap = true;
		showTextComplete = false;
		dtCharStart = DateTime.Now;
		dtSentenceStart = DateTime.Now;

		changeGBM_Index = 5;

		//Play start bgm
		bgm_Start.Play();
		
		//standard length
			//"ͬʱ������Ҫ������ؼ�ί��ί���ɳ����������ͼ��칤ί�����Ա��������Ӧ"
		
		scripts = new string[] 
		{
			"����һ��������͵����",
			"����������ʱ����ʰһ�¾ɶ�����������Щ���Զ����롣",
			"�ⲻ��Сʱ��ĺ�׻���",
			"ŶŶŶ��������Сʱ�����Ŀ�����FCսʿ����",
			"�Ҽǵ��Ǹ������ÿ����������������Сʱ�����������˻����ء�",
			"ͻȻ�䷿�俪ʼ�ض�ɽҡ",
			"��ô�ˣ�����������",
			"��׻�����һ������������˿���",
			"����������ʱ�򣬷����Լ��������Ϸ�е����˹�",
			"������ô���£�",
			"�ڰ��д�����һ��������",
			"�㽫��Զ����������磬���Ӳ����ˣ�����������",
			"���������ķ�����ȥ��Զ��������һ���İ��ĳǱ���"
		};
	}

	// Update is called once per frame
	void Update()
	{
		//If all script is shown, end up
		if (showTextComplete) return;

		//Wait between sentence
		if (inSentenceGap)
		{
			TimeSpan tsSentence = DateTime.Now - dtSentenceStart;
			if (tsSentence.TotalMilliseconds < showSentenceGap)
				return;
			else
			{
				//if whole story is done
				if (scriptIndex >= scripts.Length)
				{
					showTextComplete = true;
					fadeinoutImage.GetComponent<Animator>().SetTrigger("fadeout");
					return;
				}

				//Change BGM at specific sentence index
				if (!bgm_nervous.isPlaying && scriptIndex >= changeGBM_Index)
				{
					bgm_Start.Stop();
					bgm_nervous.Play();
				}

				//
				if (scriptIndex == 0)
					scriptImage.sprite = storyPics[0];
				else if(scriptIndex == 1)
					scriptImage.sprite = storyPics[1];
				else if (scriptIndex == 2)
					scriptImage.sprite = storyPics[2];
				else if (scriptIndex == 3)
					scriptImage.sprite = storyPics[3];
				else if (scriptIndex == 4)
					scriptImage.sprite = storyPics[4];
				else if (scriptIndex == 5)
					scriptImage.sprite = storyPics[5];
				else if (scriptIndex == 7)
					scriptImage.sprite = storyPics[6];
				else if (scriptIndex == 8)
					scriptImage.sprite = storyPics[7];
				else if (scriptIndex == 10)
					scriptImage.sprite = storyPics[8];
				else if (scriptIndex == 12)
					scriptImage.sprite = storyPics[9];


				inSentenceGap = false;
			}
		}

		//Wait between every chinese char
		TimeSpan ts = DateTime.Now - dtCharStart;
		if (ts.TotalMilliseconds > showTextGap)
		{
			dtCharStart = DateTime.Now;
			//Get substring with the times of TWO (one chinese char = 2 length)
			scriptText.text = scripts[scriptIndex].Substring(0, charIndex);
			charIndex += 1;
			//If the whole sentence is done
			if (charIndex > scripts[scriptIndex].Length)
			{
				//reset char index
				charIndex = 1;
				//go to next sentence
				scriptIndex++;

				dtSentenceStart = DateTime.Now;
				inSentenceGap = true;
			}
		}
	}
}
