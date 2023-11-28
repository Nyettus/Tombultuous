using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChasingState : EnemyStateBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CM.enemyNavMesh.isStopped = false;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        Debug.Log(defaultWalkSpeed);
        MoveToPosition(GameManager._.Master.gameObject.transform.position, defaultWalkSpeed);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
