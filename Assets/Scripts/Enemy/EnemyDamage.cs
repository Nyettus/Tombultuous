using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public float magnitude;
    public BoxCollider hitbox;

    public EnemyComponentMaster CM;
    private void Start()
    {
        if(hitbox == null) hitbox = GetComponent<BoxCollider>();

    }
    public void AssignValues(DamagePairs source)
    {
        damage = source.damage;
        magnitude = source.magnitude;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        CM.enemyAttacks.DamagePlayer(damage,(GameManager._.Master.transform.position-transform.position+Vector3.up).normalized,magnitude,CM);


    }


}
