using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIdleState : EnemyStateBase
{
    Vector3 WanderPosition;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CM.enemyNavMesh.isStopped = false;
        WanderPosition = PositionInTauros(animator.gameObject.transform.position, 10, 20);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        ;
        if (Timer(2))
        {
            WanderPosition = PositionInTauros(animator.gameObject.transform.position, 4, 10);
        }
        MoveToPosition(WanderPosition);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

    }

}
