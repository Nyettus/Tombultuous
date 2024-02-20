using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeathState : EnemyStateBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CM.enemyNavMesh.isStopped = true;
        //Vector3 away = animator.gameObject.transform.position - GameManager._.Master.gameObject.transform.position;
        //CM.FallOver(away.normalized*5f);
        Destroy(animator.gameObject, 100);
        CM.enemyRB.excludeLayers = 1<<6;
        CM.ActivateRagdoll(true);
        Destroy(CM.gameObject, 15f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //base.OnStateUpdate(animator, stateInfo, layerIndex);

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //base.OnStateExit(animator, stateInfo, layerIndex);
    }

}
