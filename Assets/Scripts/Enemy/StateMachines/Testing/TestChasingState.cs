using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

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
        var predictivePos = MurderBag.RoughPredictLocation(GameManager._.Master.gameObject.transform.position, GameManager._.Master.movementMaster.rb.velocity, thisTransform.position, CM.defaultWalkSpeed,0.3f);
        MoveToPosition(predictivePos);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
