using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelKin_RoughChase : TestRoughChaseState
{
    private float timeToCheck = 2f;
    private float timer;
    private float randomChance = 0.4f;
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
        RaycastHit hit;
        Vector3 rayDirection = GameManager._.Master.transform.position - anim.transform.position;
        if (timeToCheck <= timer)
        {
            timer = 0;
            if (Physics.Raycast(anim.transform.position, rayDirection, out hit))
            {
                if (hit.transform.tag == "Player")
                {
                    if (Random.value <= randomChance)
                        anim.SetTrigger("ProjectileAttack");
                }
            }

        }
    }
}
