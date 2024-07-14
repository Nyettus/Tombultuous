using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_Death : EnemyStateBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        var attacks = (PB_Attacks)CM.enemyAttacks;
        attacks.CombinedExplosion();
        Destroy(animator.gameObject);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
