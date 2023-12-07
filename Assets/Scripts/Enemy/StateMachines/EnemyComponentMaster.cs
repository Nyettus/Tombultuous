using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyComponentMaster : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public NavMeshAgent enemyNavMesh;
    public Animator enemyAnimator;
    public BaseEnemyAttacks enemyAttacks;
    

    public float defaultWalkSpeed = -1;


    // Start is called before the first frame update
    void Awake()
    {
        if (TryGetComponent<EnemyHealth>(out EnemyHealth healthCompono)) enemyHealth = healthCompono;
        if (TryGetComponent<NavMeshAgent>(out NavMeshAgent navCompono)) enemyNavMesh = navCompono;
        if (TryGetComponent<Animator>(out Animator animCompono)) enemyAnimator = animCompono;
        if (TryGetComponent<BaseEnemyAttacks>(out BaseEnemyAttacks damCompono)) enemyAttacks = damCompono;
    }


    public void SetAnimBool(string name, bool set)
    {
        enemyAnimator.SetBool(name, set);
    }

    public void SetAnimFloat(string name, float value = -1)
    {
        if(value == -1)
        {
            enemyAnimator.SetFloat(name, Random.Range(0f, 1f));
        }
        else
        {
            enemyAnimator.SetFloat(name, value);
        }
    }


}
