using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Unknown,
    Melee,
    Projectile,
    Hitscan,
    Item
}

public struct DamageInstance
{
    public float multipliers;
    public float baseDamage;
    public DamageType damageType;
    public float time;
    // Use parameter initialisation syntax to fill in arguements like mult after the ctor
    public DamageInstance(float baseDamage)
    {
        multipliers = 1;
        this.baseDamage = baseDamage;
        damageType = DamageType.Unknown;
        time = Time.time;
    }
}

public class EnemyHealth : MonoBehaviour, IEnemyDamageable
{
    public float health = 100f;
    public int goldAmount;

    public EnemyCountHandler countHandler;
    private bool once = true;
    private EnemyComponentMaster CM;

    private void Start()
    {
        countHandler = GetComponent<EnemyCountHandler>();
        if (TryGetComponent<EnemyComponentMaster>(out EnemyComponentMaster C)) CM = C;
    }

     public void TakeDamage(DamageInstance damage)
    {
        health -= damage.baseDamage * damage.multipliers;
        if (CM.enemyBoss != null) CM.enemyBoss.OnBossHealthChangeEvent();
        if (health <= 0 && once)
        {
            Debug.Log("im dead");
            if (GameManager._.goldManager != null) GameManager._.goldManager.GetGold(goldAmount);
            GameManager._.Master.itemMaster.onKillItemHandler.OnKill();
            countHandler.RemoveFromMaster();
            if (CM != null)
            {
                CM.enemyAnimator.SetTrigger("IsDead");

            }
            once = false;
        }

    }
    public EnemyHealth GetEnemyHealthScript()
    {
        return this;
    }

    public void FindHitboxes()
    {
        var allHitbox = GetComponentsInChildren<EnemyHitbox>();
        if (allHitbox.Length == 0)
        {
            Debug.LogWarning("No Hitboxes found");
            return;
        }

        Debug.Log("" + allHitbox.Length + " Hitboxes Found");
        foreach (EnemyHitbox hitbox in allHitbox)
        {
            hitbox.host = this;
            
        }


    }

}
