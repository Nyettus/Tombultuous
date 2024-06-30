using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyDamageable
{
    public void TakeDamage(DamageInstance damage);
    /// <summary>
    /// Returns the enemies sole health manager (used to determine if attack hits the same enemy multiple times)
    /// </summary>
    /// <returns></returns>
    public EnemyHealth GetEnemyHealthScript();

}
