using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

public class TestAttackState : EnemyStateBase
{
    Vector3 torusCoords;
    float randomSign;
    float distance = 2.4f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CM.SetAnimFloat("AttackChance");
        var holding = (thisTransform.position - GameManager._.Master.gameObject.transform.position);
        torusCoords = new Vector3(holding.x, holding.z, distance);
        randomSign = Mathf.Sign(Random.Range(-1, 1));
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        
        torusCoords = MurderBag.OrbitCoords(torusCoords, MurderBag.OrbitOptimalDeg(distance,CM.defaultWalkSpeed) * randomSign);

        MoveToPosition(NavmeshTorus(GameManager._.Master.gameObject.transform.position, torusCoords));
        FaceTarget(GameManager._.Master.gameObject.transform.position, 10f);

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        base.OnStateExit(animator, stateInfo, layerIndex);
        MoveToPosition(GameManager._.Master.gameObject.transform.position, 0f);
        FaceTarget(GameManager._.Master.gameObject.transform.position, 10f);

    }



}
