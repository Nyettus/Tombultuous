using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRootMotion : EnemyStateBase
{
    public string variableName = null;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if(variableName != null)
        {
            animator.SetFloat(variableName, Random.value);
        }
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
