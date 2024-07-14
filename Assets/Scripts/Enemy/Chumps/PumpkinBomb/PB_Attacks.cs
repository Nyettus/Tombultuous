using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PB_Attacks : BaseEnemyAttacks
{
    [Header("Explosion Stats")]
    [SerializeField] private float range;

    

    //Linear lerp for distance (min damage 0)
    public void PB_BombAttack()
    {
        BombSelf();
    }

    public void CombinedExplosion()
    {
        BombPlayer();
        BombAlly();
        var explosion = ObjectPooler._.SpawnFromPool("PumpkinExplosion", CM.enemyAnimator.rootPosition, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
    }


    private void BombAlly()
    {
        Collider[] colliderArray = Physics.OverlapSphere(gameObject.transform.position, range);
        foreach(Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out EnemyHealth health))
            {
                var dmg = CalcDamage(health.gameObject.transform.position);
                var dmgInstance = new DamageInstance(dmg);
                health.TakeDamage(dmgInstance);
            }
        }
    }
    private void BombPlayer()
    {
        var playerPos = GameManager._.Master.transform.position;
        var dmg = CalcDamage(playerPos);
        var knockbackDir = (playerPos - transform.position + Vector3.up*2).normalized;
        var knockbackAmount = damageValues.damageArray[0].magnitude * DistAsPercent(playerPos);
        if ((int)dmg == 0) return;
        DamagePlayer((int)dmg, knockbackDir, knockbackAmount);


    }
    private void BombSelf()
    {
        var dmg = new DamageInstance(CM.card.health * 5);
        CM.enemyHealth.TakeDamage(dmg);
    }

    private float CalcDamage(Vector3 targetLocation)
    {
        var distAsPercent = DistAsPercent(targetLocation);
        var damage = damageValues.damageArray[0].damage * distAsPercent;
        return damage;

    }

    private float DistAsPercent(Vector3 targetPos)
    {
        var startPos = CM.enemyAnimator.rootPosition;
        var rawDistance = Vector3.Distance(startPos, targetPos);
        if (rawDistance > range)
        {
            Debug.Log("Target Out of range");
            return 0;
        }
        var distAsPercent = Mathf.Clamp(1 - (rawDistance / range), 0, 1);
        return distAsPercent;
    }
}
