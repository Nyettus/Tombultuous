using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanWeaponBase : RangedWeaponBase
{
    public override void Shoot()
    {
        base.Shoot();
        for(int i = 0; i < bullets; i++)
        {
            RaycastHit hit;
            Vector3 shootDir = bulletSpread(spread);
            if (Physics.Raycast(Camera.main.transform.position,shootDir,out hit,100f,layerMask))
            {
                HitscanHit(hit);
             
            }

        }

    }

    protected void HitscanHit(RaycastHit hit)
    {
        if (hit.transform.tag == "Enemy")
        {
            float damage = damageFalloff(hit.distance) * GameManager._.Master.weaponMaster.damageMult;
            hit.transform.GetComponent<EnemyHealth>().takeDamage(damage);
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
        Debug.DrawLine(Camera.main.transform.position, endPos,Color.red,2.5f);
    }
}
