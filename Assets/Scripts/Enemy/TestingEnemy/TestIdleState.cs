using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

public class TestIdleState : EnemyStateBase
{
    Vector3 WanderPosition;
    float randomTime = 2;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CM.enemyNavMesh.isStopped = false;
        WanderPosition = NavmeshTorus(thisTransform.position, MurderBag.RandomTorusCoords(10, 20));
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (Timer(randomTime))
        {
            randomTime = Random.Range(0, 5);
            WanderPosition = MurderBag.PositionInTorus(thisTransform.position, MurderBag.RandomTorusCoords(4, 10));
        }
        MoveToPosition(WanderPosition,CM.defaultWalkSpeed*0.5f);
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

    }

}
