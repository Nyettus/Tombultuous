using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRootMotionHandler : MonoBehaviour
{
    public EnemyComponentMaster CM;

    //Hark, this is the reason the duct tape is here.
    //It is because unity is big stinky adding OnAnimatorMove breaks root bones that arent floored.
    private void OnAnimatorMove()
    {
        if (CM.enemyAnimator.applyRootMotion)
        {
            CM.enemyNavMesh.updatePosition = false;
            Vector3 rootPos = CM.enemyAnimator.rootPosition;
            rootPos.y = CM.enemyNavMesh.nextPosition.y;
            transform.position = rootPos;
            CM.enemyNavMesh.nextPosition = rootPos;
        }
        else
        {
            CM.enemyNavMesh.updatePosition = true;
        }


    }
}
