using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LG_Spin : BarrelKin_Charge
{
    LG_Attacks attacks;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        base.OnStateEnter(animator, stateInfo, layerIndex);
        attacks = (LG_Attacks)CM.enemyAttacks;
        attacks.LG_Spin_ON();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);


    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        attacks.LG_Spin_OFF();
        CM.enemyNavMesh.speed = CM.defaultWalkSpeed;
    }
}
