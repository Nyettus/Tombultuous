using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFacingAttackState : EnemyStateBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CM.enemyNavMesh.velocity = Vector3.zero;
        CM.enemyNavMesh.isStopped = true;
        FaceTarget(GameManager._.Master.gameObject.transform.position, 10000000f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CM.enemyNavMesh.isStopped = false;
        CM.enemyNavMesh.velocity = Vector3.zero;
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
