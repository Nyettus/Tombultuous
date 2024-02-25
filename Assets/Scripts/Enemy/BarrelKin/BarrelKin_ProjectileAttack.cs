using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

public class BarrelKin_ProjectileAttack : EnemyStateBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        var quickref = GameManager._.Master;
        float randomLerp = Random.value;
        var targetLocation = MurderBag.RoughPredictLocation(
            quickref.transform.position+Vector3.up*(quickref.movementMaster.height/4),
            quickref.movementMaster.rb.velocity,
            animator.transform.position,
            10,
            randomLerp);
        
        var spawnLocation = animator.transform.position + Vector3.up * 3;
        FireProjectile("BK_Proj", targetLocation, spawnLocation);
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
