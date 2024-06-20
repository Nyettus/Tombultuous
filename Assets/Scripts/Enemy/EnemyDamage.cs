using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public float magnitude;
    public Collider hitbox;
    public bool canHit = true;
    private Vector3 knockbackDir;

    public EnemyComponentMaster CM;
    private void Start()
    {
        if(hitbox == null) hitbox = GetComponent<Collider>();

    }
    public void AssignValues(DamagePairs source)
    {
        damage = source.damage;
        magnitude = source.magnitude;
        knockbackDir = (GameManager._.Master.transform.position - transform.position + Vector3.up).normalized;
    }
    public void AssignValues(DamagePairs source, Vector3 customDir)
    {
        AssignValues(source);
        knockbackDir = customDir;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" || !canHit) return;
        CM.enemyAttacks.DamagePlayer(damage,knockbackDir,magnitude,CM);
        canHit = false;


    }


}
