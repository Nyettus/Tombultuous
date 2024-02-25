using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelKin_RoughChase : TestRoughChaseState
{
    private float timeToCheck = 2f;
    private float timer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        CheckProjectile(animator);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }


    private void CheckProjectile(Animator anim)
    {
        timer += Time.deltaTime;
        if (timeToCheck <= timer)
        {
            timer = 0;
            anim.SetTrigger("ProjectileAttack");
        }
    }
}
