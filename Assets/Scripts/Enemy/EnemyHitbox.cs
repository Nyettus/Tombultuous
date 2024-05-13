using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour, IEnemyDamageable
{
    public EnemyHealth host;
    public float multiplier = 1f;

    public void TakeDamage(float damage)
    {
        if (host == null)
        {
            Debug.LogError("Hitbox Host Not Assigned");
            return;
        }
        Debug.Log("Hitbox hit at x" + multiplier + " damage");
        host.TakeDamage(damage * multiplier);
    }
}
