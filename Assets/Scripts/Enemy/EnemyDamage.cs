using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private float magnitude;

    [SerializeField]
    private EnemyComponentMaster CM;
    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        CM.enemyAttacks.DamagePlayer(damage,(GameManager._.Master.transform.position-transform.position+Vector3.up).normalized,magnitude);


    }
}
