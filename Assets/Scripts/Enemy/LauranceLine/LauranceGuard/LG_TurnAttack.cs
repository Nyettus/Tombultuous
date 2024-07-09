using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;


public class LG_TurnAttack : EnemyRootMotion
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        int currentConsecutive = animator.GetInteger("ConsecutiveAttacks");
        animator.SetInteger("ConsecutiveAttacks", currentConsecutive + 1);
        Vector3 target = MurderBag.RoughPredictLocation(
            GameManager._.Master.transform.position,
            GameManager._.Master.movementMaster.rb.velocity,
            animator.transform.position,
            5f);
        FaceTarget(target, 30f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
