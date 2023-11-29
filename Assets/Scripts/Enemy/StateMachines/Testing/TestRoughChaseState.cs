using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoughChaseState : EnemyStateBase
{
    Vector3 WanderPosition;
    Vector3 lockedTorusCoords;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        lockedTorusCoords = RandomTorusCoords(4, 10);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (Timer(2))
        {
            lockedTorusCoords = RandomTorusCoords(4, 5);
        }
        WanderPosition = PositionInTorus(GameManager._.Master.gameObject.transform.position, lockedTorusCoords);
        MoveToPosition(WanderPosition);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
