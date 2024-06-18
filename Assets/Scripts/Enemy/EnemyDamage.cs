using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public float magnitude;

    [SerializeField]
    private BoxCollider hitbox;

    public EnemyComponentMaster CM;
    private void Start()
    {
        hitbox = GetComponent<BoxCollider>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        CM.enemyAttacks.DamagePlayer(damage,(GameManager._.Master.transform.position-transform.position+Vector3.up).normalized,magnitude,CM);


    }



}
