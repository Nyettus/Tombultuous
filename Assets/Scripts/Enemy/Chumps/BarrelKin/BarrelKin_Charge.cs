using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelKin_Charge : TestChasingState
{
    private float acceleration;
    public float multiplier = 4;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        acceleration = CM.enemyNavMesh.acceleration;
        CM.enemyNavMesh.speed = CM.defaultWalkSpeed* multiplier;
        CM.enemyNavMesh.acceleration = acceleration * multiplier;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (CM.enemyNavMesh.speed != CM.defaultWalkSpeed * multiplier) CM.enemyNavMesh.speed = CM.defaultWalkSpeed * multiplier;
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
