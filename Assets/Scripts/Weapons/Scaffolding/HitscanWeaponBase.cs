using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanWeaponBase : RangedWeaponBase
{
    [Header("Hitscan Traits")]
    protected float maxDamage;
    protected float minDamage;
    protected float minRange;
    protected float maxRange;

    protected override void Establish()
    {
        base.Establish();
        HitscanGun hitscan = (HitscanGun)card;
        maxDamage = hitscan.maxDamage;
        minDamage = hitscan.minDamage;
        minRange = hitscan.minRange;
        maxRange = hitscan.maxRange;
    }


    public override void Shoot()
    {
        base.Shoot();
        for (int i = 0; i < bullets; i++)
        {
            RaycastHit hit;
            Vector3 shootDir = bulletSpread(spread);
            if (Physics.Raycast(Camera.main.transform.position, shootDir, out hit, 100f, layerMask))
            {
                HitscanHit(hit);

            }

        }

    }

    protected void HitscanHit(RaycastHit hit)
    {
        if (hit.transform.tag == "Enemy")
        {
            Debug.Log(hit.transform.gameObject.name);
            float damage = damageFalloff(hit.distance);

            if (hit.collider.transform.TryGetComponent(out IEnemyDamageable hitboxHealth))
            {
                var dmg = new DamageInstance(damage) { multipliers = GameManager._.Master.weaponMaster.damageMult };
                hitboxHealth.TakeDamage(dmg);
            }
            else
            {
                Debug.LogError("NO HITBOX OF ANY SORT FOUND");
            }
            GameManager._.Master.itemMaster.onHitEffectHandler.OnHitEffect(hit.point);
        }
        else if (hit.transform.tag == "ItemChest")
        {
            hit.transform.GetComponent<ChestCore>().SpawnItem();
        }
        else if (hit.transform.tag == "MasterTomb")
        {
            hit.transform.GetComponentInParent<MasterTomb>().DestroySelf();
        }
        else if (hit.transform.TryGetComponent(out NextLevel level))
        {
            level.GotoNextLevel();
        }
        else
        {
            SummonWallHit(hit);
            GameManager._.Master.itemMaster.onMissEffectHandler.OnMissEffect(hit.point);
        }
        summonTracer(hit);
    }

    protected void summonTracer(RaycastHit hit)
    {
        GameObject tracer = ObjectPool.SpawnFromPool("Tracer", Camera.main.transform.position, transform.rotation);
        if (tracer.TryGetComponent(out HitscanTracers TracerScript))
        {
            TracerScript.EstablishTrails(transform.position, hit.point);
        }
    }

    protected void SummonWallHit(RaycastHit hit)
    {
        GameObject Splash = ObjectPool.SpawnFromPool("Splash", hit.point, Quaternion.LookRotation(hit.normal));
        if (Splash.TryGetComponent(out WallHitEffect SplashScript))
        {
            SplashScript.Establish();
        }
    }


    public void rayLine(Vector3 endPos)
    {
        Debug.DrawLine(Camera.main.transform.position, endPos, Color.red, 2.5f);
    }


    protected float damageFalloff(float distance)
    {
        float normalised;
        normalised = Mathf.Clamp((distance - minRange) / (maxRange - minRange), 0f, 1f);
        return normalised * minDamage + (1 - normalised) * maxDamage;
    }
}
