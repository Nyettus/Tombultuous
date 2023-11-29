using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoughChaseState : EnemyStateBase
{
    Vector3 WanderPosition;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        WanderPosition = PositionInTauros(GameManager._.Master.gameObject.transform.position, 4, 10);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (Timer(2))
        {
            WanderPosition = PositionInTauros(GameManager._.Master.gameObject.transform.position, 1, 5);
        }
        MoveToPosition(WanderPosition);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
