using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    protected int damage;
    [SerializeField]
    private float magnitude;

    [SerializeField]
    protected EnemyComponentMaster CM;
    private void Start()
    {
        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        CM.enemyAttacks.DamagePlayer(damage,(GameManager._.Master.transform.position-transform.position+Vector3.up).normalized,magnitude,CM);


    }



}
