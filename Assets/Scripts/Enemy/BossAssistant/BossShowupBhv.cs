using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShowupBhv : StateMachineBehaviour
{
    private GameObject boss;
    private Vector2 targetPos;
    private bool isArrived;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossAssistantController>().Boss;
        boss.transform.position = new Vector2(6.9f, 15f);
        targetPos = new Vector2(6.9f, 1.01f);
        isArrived = false;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isArrived) return;

        if (Vector2.Distance(boss.transform.position, targetPos) > 0.1f)
            boss.transform.position = Vector2.MoveTowards(boss.transform.position, targetPos, 15f * Time.deltaTime);
        else
        {
            boss.transform.position = targetPos;
            isArrived = true;
            //show dialog
            animator.GetComponent<BossAssistantController>().DialogArea.SetActive(true);
            //animator.SetTrigger("hpshowup");
        }
    }
}
