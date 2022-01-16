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
			//"同时，该市要求各区县纪委监委、派出各开发区纪检监察工委抽调人员，成立相应"
		
		scripts = new string[] 
		{
			"这是一个宁静祥和的午后",
			"今天终于有时间收拾一下旧东西，看看哪些可以断舍离。",
			"这不是小时候的红白机吗？",
			"哦哦哦！这是我小时候最爱玩的卡带【FC战士】！",
			"我记得那个暑假我每天至少能玩上三个小时，还真是令人怀念呢。",
			"突然间房间开始地动山摇",
			"怎么了！？地震了吗？",
			"红白机闪出一道光把你吸入了卡带",
			"当你醒来的时候，发现自己变成了游戏中的主人公",
			"这是怎么回事？",
			"黑暗中传来了一个声音：",
			"你将永远留在这个世界，你逃不掉了，哈哈哈哈！",
			"向着声音的方向望去，远方出现了一座幽暗的城堡。"
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
