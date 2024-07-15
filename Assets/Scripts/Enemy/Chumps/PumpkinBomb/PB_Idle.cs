using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_Idle : TestIdleState
{
    private float timeUntilChase;
    private float currentTime = 0;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        timeUntilChase = Random.Range(30f, 60f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        currentTime += Time.deltaTime;
        if (currentTime > timeUntilChase)
        {
            animator.SetBool("IsChasing", true);
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
