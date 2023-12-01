using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyStateBase : StateMachineBehaviour
{
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
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(CM.defaultWalkSpeed);
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
    protected void MoveToPosition(Vector3 position, float speed = float.MaxValue)
    {
        if (speed == float.MaxValue) speed = CM.defaultWalkSpeed;

        var holding = CM.enemyNavMesh;
        holding.speed = speed;
        holding.destination = position;
    }

    /// <summary>
    /// Smoothly face target position at speed rate
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rate"></param>
    protected void FaceTarget(Vector3 position, float rate)
    {
        var targetRot = Quaternion.LookRotation(position - thisTransform.position);
        thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, targetRot, rate * Time.deltaTime);
    }

    /// <summary>
    /// Calculate a random point in a torus in normalised cartesian with magnitude
    /// </summary>
    /// <param name="lowerBound">The first integer.</param>
    /// <param name="upperBound">The second integer.</param>
    /// <returns>The Vector3 formatted (direction x, direction z, magnitude.</returns>
    protected Vector3 RandomTorusCoords(float lowerBound, float upperBound)
    {
        var randomDir = Random.insideUnitCircle.normalized;
        var randomDist = Random.Range(lowerBound, upperBound);
        return new Vector3(randomDir.x,randomDir.y, randomDist);
    }

    /// <summary>
    /// Calculate the navmesh position of point in a torus around the starting position
    /// </summary>
    /// <param name="startLocation"></param>
    /// <param name="torusCoords"></param>
    /// <returns>The navmesh point or the starting location if no navmesh point is found </returns>
    protected Vector3 PositionInTorus(Vector3 startLocation, Vector3 torusCoords)
    {
        var flatStart = new Vector2(startLocation.x, startLocation.z);
        var randomDir = new Vector2(torusCoords.x,torusCoords.y);
        var randomDist = torusCoords.z;
        var v2position = flatStart + randomDir * randomDist;
        var worldspacev3 = new Vector3(v2position.x, startLocation.y, v2position.y);
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(worldspacev3, out navHit, 10, -1))
        {
            return navHit.position;
        }
        else
            return startLocation;
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
