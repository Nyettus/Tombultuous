using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanWeaponBase : RangedWeaponBase
{
    public override void Shoot()
    {
        for(int i = 0; i < bullets; i++)
        {
            RaycastHit hit;
            Vector3 shootDir = bulletSpread(spread);
            if(Physics.Raycast(Camera.main.transform.position,shootDir,out hit,100f,~(1<<6)))
            {
                if(hit.transform.tag == "Enemy")
                {
                    float damage = damageFalloff(hit.distance) * GameManager._.Master.weaponMaster.damageMult;
                    hit.transform.GetComponent<EnemyHealth>().takeDamage(damage);
                }
                else if(hit.transform.tag == "ItemChest")
                {
                    hit.transform.GetComponent<ChestCore>().SpawnItem();
                }

                GameObject tracer = ObjectPool.SpawnFromPool("Tracer", Camera.main.transform.position, transform.rotation);
                if(tracer.TryGetComponent(out HitscanTracers script))
                {
                    script.EstablishTrails(transform.position, hit.point);
                }

            }

        }

    }


    public void rayLine(Vector3 endPos)
    {
        Debug.DrawLine(Camera.main.transform.position, endPos,Color.red,2.5f);
    }
}
