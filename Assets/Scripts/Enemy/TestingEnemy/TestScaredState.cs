using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

public class TestScaredState : EnemyStateBase
{
    Vector3 torusCoords;
    float randomSign;
    float distance = 8f;
    float randomEndTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CM.SetAnimFloat("AttackChance");
        Debug.Log("Im going to shank you bruv");
        var holding = (thisTransform.position - GameManager._.Master.gameObject.transform.position);
        torusCoords = new Vector3(holding.x, holding.z, distance);
        randomSign = Mathf.Sign(Random.Range(-1, 1));
        randomEndTime = Random.Range(1f, 3f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        torusCoords = MurderBag.OrbitCoords(torusCoords, MurderBag.OrbitOptimalDeg(distance, CM.defaultWalkSpeed) * randomSign);

        MoveToPosition(NavmeshTorus(GameManager._.Master.gameObject.transform.position, torusCoords));
        FaceTarget(GameManager._.Master.gameObject.transform.position, 100f);

        if (Timer(randomEndTime))
        {
            randomSign = Mathf.Sign(Random.Range(-1, 1));
            var leaveChance = Random.Range(0f, 1f);
            if (leaveChance > 0.8f)
            {
                animator.SetTrigger("IsConfident");

            }
            else
            {
                randomEndTime = Random.Range(1f, 3f);
            }

        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        base.OnStateExit(animator, stateInfo, layerIndex);
        MoveToPosition(GameManager._.Master.gameObject.transform.position, 0f);
        FaceTarget(GameManager._.Master.gameObject.transform.position, 100f);

    }
}
