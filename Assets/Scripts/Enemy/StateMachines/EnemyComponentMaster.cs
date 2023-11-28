using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyComponentMaster : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public NavMeshAgent enemyNavMesh;
    public Animator enemyAnimator;


    // Start is called before the first frame update
    void Awake()
    {
        if (TryGetComponent<EnemyHealth>(out EnemyHealth healthCompono)) enemyHealth = healthCompono;
        if (TryGetComponent<NavMeshAgent>(out NavMeshAgent navCompono)) enemyNavMesh = navCompono;
        if (TryGetComponent<Animator>(out Animator animCompono)) enemyAnimator = animCompono;
    }


    public void SetAnimBool(string name, bool set)
    {
        enemyAnimator.SetBool(name, set);
    }


}
