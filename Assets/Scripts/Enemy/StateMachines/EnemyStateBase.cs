using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UsefulBox;

public abstract class EnemyStateBase : StateMachineBehaviour
{
    public bool UsesRootMotion = false;
    protected EnemyComponentMaster CM;
    protected Transform thisTransform;

    protected float incrementTimer = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CM = animator.GetComponent<EnemyComponentMaster>();
        if(CM.defaultWalkSpeed == -1) CM.defaultWalkSpeed = CM.enemyNavMesh.speed;
        if (CM.enemyNavMesh.speed != CM.defaultWalkSpeed) CM.enemyNavMesh.speed = CM.defaultWalkSpeed;
        thisTransform = animator.gameObject.transform;
        CM.enemyNavMesh.isStopped = false;
        CM.SetRootMotion(UsesRootMotion);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    /// <summary>
    /// Move navmesh agent towards position at speed (default speed becomes default navmesh speed)
    /// </summary>
    /// <param name="position"></param>
    /// <param name="speed"></param>
    protected void MoveToPosition(Vector3 position, float speed)
    {
        var holding = CM.enemyNavMesh;
        holding.speed = speed;
        NavMeshHit navHit;
        if(NavMesh.SamplePosition(position,out navHit, 2, -1))
        {
            holding.destination = navHit.position;
        }
        else
        {
            holding.destination = position;
        }


    }
    protected void MoveToPosition(Vector3 position)
    {
        var speed = CM.defaultWalkSpeed;
        MoveToPosition(position, speed);
    }


    /// <summary>
    /// Smoothly face target position at speed rate
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rate"></param>
    protected void FaceTarget(Vector3 position, float rate)
    {
        Vector3 adjustPos = position - thisTransform.position;
        adjustPos = new Vector3(adjustPos.x, 0, adjustPos.z);
        Quaternion targetRot = Quaternion.LookRotation(adjustPos);
        thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, targetRot, rate * Time.deltaTime);
    }




    protected Vector3 NavmeshTorus(Vector3 startLocation, Vector3 torusCoords)
    {
        var worldspacev3 = MurderBag.PositionInTorus(startLocation, torusCoords);
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(worldspacev3, out navHit, 2, -1))
        {
            return navHit.position;
        }
        else
            return startLocation;
    }

    protected void FireProjectile(string tag, Vector3 target, Vector3 source)
    {
        Vector3 dir = target-source;
        Quaternion aimDirection = Quaternion.LookRotation(dir, Vector3.up);
        
        GameObject spawnedProjectile = ObjectPooler._.SpawnFromPool(tag, source, aimDirection);
        spawnedProjectile.GetComponent<ProjectileManager>().Initialise(source, aimDirection);
    }


    protected bool Timer(float timeLimit)
    {
        if(incrementTimer < timeLimit)
        {
            incrementTimer += Time.deltaTime;
            return false;
        }
        else
        {
            incrementTimer = 0;
            return true;
        }

    }


}
