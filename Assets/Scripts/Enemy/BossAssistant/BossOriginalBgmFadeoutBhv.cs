using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOriginalBgmFadeoutBhv : StateMachineBehaviour
{
    private GameObject originalMusic;
    private AudioSource audioBGMusic;
    float deltaVolume;
    private bool isPlayingOriginalbgm;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //disable player control
        GlobalVars.IsPlayerControllable = false;

        originalMusic = GameObject.FindGameObjectWithTag("bgmusic");
        if (originalMusic != null)
            audioBGMusic = originalMusic.GetComponent<AudioSource>();
        isPlayingOriginalbgm = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isPlayingOriginalbgm && audioBGMusic == null) return;

        deltaVolume = 0.2f * Time.deltaTime;
        if (audioBGMusic.volume - deltaVolume > 0.01f)
            audioBGMusic.volume = audioBGMusic.volume - deltaVolume;
        else
        {
            audioBGMusic.Stop();
            isPlayingOriginalbgm = false;
            animator.SetTrigger("warning");
        }
    }
}
