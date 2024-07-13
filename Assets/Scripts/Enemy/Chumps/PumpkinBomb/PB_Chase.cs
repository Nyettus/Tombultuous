using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_Chase : TestChasingState
{
    private float timer;
    private float multiplier = 0.2f;
    private NavMeshAgentValues baseValues;
    private float maxSpeed = 10;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        baseValues = new NavMeshAgentValues(CM.enemyNavMesh.angularSpeed, CM.enemyNavMesh.acceleration);

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        timer += Time.deltaTime;
        float speedMultiplier = Mathf.Clamp(timer * multiplier,1,maxSpeed);
        animator.SetFloat("MoveSpeed", speedMultiplier);

        CM.enemyNavMesh.speed = CM.defaultWalkSpeed * speedMultiplier;
        CM.enemyNavMesh.angularSpeed = baseValues.angularSpeed * speedMultiplier;
        CM.enemyNavMesh.acceleration = baseValues.acceleration * speedMultiplier;


    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        CM.enemyNavMesh.angularSpeed = baseValues.angularSpeed;
        CM.enemyNavMesh.acceleration = baseValues.acceleration;
    }
}
