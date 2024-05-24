using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LG_Charge : EnemyStateBase
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        base.OnStateEnter(animator, stateInfo, layerIndex);
        MoveToPosition(animator.gameObject.transform.position, CM.enemyNavMesh.speed);


    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        bool charge = animator.GetBool("DecisionBool");
        if (charge)
        {
            float chargeSpeed = 5;
            Vector3 dir = animator.transform.position + animator.transform.forward;
            MoveToPosition(dir, chargeSpeed);
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        MoveToPosition(animator.gameObject.transform.position, CM.enemyNavMesh.speed);
        animator.SetBool("DecisionBool", false);
    }
}
