using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public EnemyHealth host;
    public float multiplier = 1f;

    public void CastToHost(float damage)
    {
        host.TakeDamage(damage * multiplier);
    }
}
