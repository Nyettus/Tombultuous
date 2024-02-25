using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Projectile Type", menuName = "Weapons/Projectile/Projectile Type")]
public class ProjectileType : ScriptableObject
{
    public bool ally;
    public bool useGravity;
    public bool piercing;

    public float speed;

    public float baseDamage;

    public void ProjDamage(Transform thisPos, PlayerHealth playerHealth = null)
    {
        if (ally) return;
        if (playerHealth == null)
        {
            Debug.LogError("Player health is null");
            return;
        }
        playerHealth.takeDamage((int)baseDamage, thisPos.position, 0);

    }


    public void ProjDamage(EnemyHealth enemyHealth = null)
    {
        if (!ally) return;
        if (enemyHealth == null)
        {
            Debug.LogError("Enemy health is null");
            return;
        }
        enemyHealth.takeDamage(baseDamage * GameManager._.Master.weaponMaster.damageMult);


    }


}
