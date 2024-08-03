using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Projectile Type", menuName = "Weapons/Projectile/Projectile Type")]
public class ProjectileType : ScriptableObject
{
    public bool ally;
    public bool useGravity;
    public int pierceCount;
    public int bounceCount;

    public float speed;

    public float baseDamage;

    public void ProjDamage(Transform thisPos, PlayerHealth playerHealth = null)
    {
        if (playerHealth == null)
        {
            Debug.LogError("Player health is null");
            return;
        }
        playerHealth.takeDamage((int)baseDamage, thisPos.position, 0);

    }


    public void ProjDamage(IEnemyDamageable enemyHitbox = null)
    {
        if (enemyHitbox == null)
        {
            Debug.LogError("Enemy health is null");
            return;
        }
        var dmg = new DamageInstance(baseDamage)
        {
            multipliers = GameManager._.Master.weaponMaster.damageMult,
            damageType = DamageType.Projectile
        };
        enemyHitbox.TakeDamage(dmg);

    }

}
