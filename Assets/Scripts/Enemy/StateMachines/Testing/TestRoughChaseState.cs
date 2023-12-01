using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoughChaseState : EnemyStateBase
{
    Vector3 WanderPosition;
    Vector3 lockedTorusCoords;
    float randomTime = 2;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        lockedTorusCoords = RandomTorusCoords(4, 10);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (Timer(randomTime))
        {
            randomTime = Random.Range(0, 5);
            var distance = Vector3.Distance(thisTransform.position, GameManager._.Master.gameObject.transform.position)/2;
            lockedTorusCoords = RandomTorusCoords(Mathf.Clamp(distance-1,0,100), distance);
        }
        WanderPosition = PositionInTorus(GameManager._.Master.gameObject.transform.position, lockedTorusCoords);
        MoveToPosition(WanderPosition);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
