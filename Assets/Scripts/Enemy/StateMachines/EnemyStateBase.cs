using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStateBase : StateMachineBehaviour
{
    protected EnemyComponentMaster CM;
    protected float defaultWalkSpeed = -1;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CM = animator.GetComponent<EnemyComponentMaster>();
        if(defaultWalkSpeed == -1) defaultWalkSpeed = CM.enemyNavMesh.speed;
        if (CM.enemyNavMesh.speed != defaultWalkSpeed) CM.enemyNavMesh.speed = defaultWalkSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    protected void MoveToPosition(Vector3 position, float speed)
    {
        var holding = CM.enemyNavMesh;
        holding.speed = speed;
        holding.destination = position;
    }

    protected void FaceTarget(Vector3 position)
    {
        CM.gameObject.transform.LookAt(position);
    }

}
