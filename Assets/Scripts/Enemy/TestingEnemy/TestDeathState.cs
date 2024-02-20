using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

public class TestDeathState : EnemyStateBase
{
    public ItemPools spawnable;
    public float itemDropChange = 1f;
    public Vector3 offset;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CM.enemyNavMesh.isStopped = true;
        //Vector3 away = animator.gameObject.transform.position - GameManager._.Master.gameObject.transform.position;
        //CM.FallOver(away.normalized*5f);
        Destroy(animator.gameObject, 15);
        CM.enemyRB.excludeLayers = 1<<6 | 1<<12;
        CM.ActivateRagdoll(true);
        DropItem();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //base.OnStateUpdate(animator, stateInfo, layerIndex);

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //base.OnStateExit(animator, stateInfo, layerIndex);
    }


    private void DropItem()
    {
        ItemBase itemToSpawn;
        if (Random.value <= itemDropChange)
        {
            itemToSpawn = MoneySack.FindItem(spawnable);
            Instantiate(itemToSpawn.prefab, CM.transform.position+offset, CM.transform.rotation);
        }
    }

}
