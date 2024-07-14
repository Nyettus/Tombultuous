using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_Bomb : EnemyStateBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        CM.enemyNavMesh.speed = CM.defaultWalkSpeed * animator.GetFloat("MoveSpeed");
        RunForward();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }

    private void RunForward()
    {
        FaceTarget(GameManager._.Master.transform.position, 5);
        CM.enemyNavMesh.destination = thisTransform.forward + thisTransform.position;


    }
}
