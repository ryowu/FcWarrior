using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWarningBhv : StateMachineBehaviour
{
    DateTime dtStart;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dtStart = DateTime.Now;
        AudioSource[] acs = animator.GetComponents<AudioSource>();
        foreach (AudioSource ac in acs)
        {
            if (ac.clip.name.IndexOf("Warning_Loop1") > -1)
                ac.Play();
        }
    }
}
