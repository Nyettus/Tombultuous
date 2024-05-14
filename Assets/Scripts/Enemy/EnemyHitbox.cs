using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour, IEnemyDamageable
{
    public EnemyHealth host;
    public float multiplier = 1f;

    public EnemyHealth GetEnemyHealthScript()
    {
        return host;
    }

    public void TakeDamage(float damage)
    {
    }

    public void TakeDamage(DamageInstance damage)
    {
        if (host == null)
        {
            Debug.LogError("Hitbox Host Not Assigned");
            return;
        }
        Debug.Log("Hitbox hit at x" + multiplier + " damage");
        damage.multipliers *= multiplier;
        host.TakeDamage(damage);
    }
}
