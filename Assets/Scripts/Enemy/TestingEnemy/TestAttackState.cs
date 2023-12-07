using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttackState : EnemyStateBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CM.SetAnimFloat("AttackChance");
        Debug.Log("Im going to shank you bruv");
        CM.enemyNavMesh.isStopped = true;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        MoveToPosition(GameManager._.Master.gameObject.transform.position, 0);
        FaceTarget(GameManager._.Master.gameObject.transform.position, 7.5f);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        Debug.Log("You get away this time but next time ill get yu bruv");
        CM.enemyNavMesh.isStopped = false;
    }
}
