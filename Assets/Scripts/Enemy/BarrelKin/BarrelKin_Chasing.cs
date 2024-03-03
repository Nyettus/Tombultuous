using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelKin_Chasing : TestChasingState
{
    private float timeToCheck = 2f;
    private float timer;
    private float randomChance = 0.8f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CM.enemyNavMesh.speed = CM.defaultWalkSpeed;
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        if(CM.enemyNavMesh.speed != CM.defaultWalkSpeed) CM.enemyNavMesh.speed = CM.defaultWalkSpeed;
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        CheckCharge(animator);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }

    private void CheckCharge(Animator anim)
    {
        timer += Time.deltaTime;
        if (timer > timeToCheck)
        {
            Debug.Log("Timer Up");
            if (Random.value <= randomChance)
            {
                anim.SetTrigger("ChargeAttack");
            }
            timer = 0;
        }
    }
}
