using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttackState : EnemyStateBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        Debug.Log("Im going to shank you bruv");

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        var tinyLerp = Vector3.Lerp(GameManager._.Master.gameObject.transform.position, animator.gameObject.transform.position, 0.95f);
        MoveToPosition(tinyLerp, defaultWalkSpeed);
        FaceTarget(tinyLerp);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        CM.enemyNavMesh.updatePosition = true;
        Debug.Log("You get away this time but next time ill get yu bruv");
    }
}
