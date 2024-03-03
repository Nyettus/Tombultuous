using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelKin_Charge : TestChasingState
{
    private float acceleration;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        acceleration = CM.enemyNavMesh.acceleration;
        CM.enemyNavMesh.speed = CM.defaultWalkSpeed*4f;
        CM.enemyNavMesh.acceleration = acceleration * 4f;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (CM.enemyNavMesh.speed != CM.defaultWalkSpeed * 4f) CM.enemyNavMesh.speed = CM.defaultWalkSpeed * 4f;
        RunForward();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        CM.enemyNavMesh.acceleration = acceleration;

    }

    private void RunForward()
    {
        FaceTarget(GameManager._.Master.transform.position, 10);
        CM.enemyNavMesh.destination = thisTransform.forward+thisTransform.position;
        

    }


}
