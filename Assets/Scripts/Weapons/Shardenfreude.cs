using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shardenfreude : HitscanWeaponBase
{

    //A shotgun of remaining shots
    public override void Special()
    {
        if (!specialUsed&&curMag!=0)
        {
            specialCooldown = curMag*0.5f;

            for (int i = 0; i < curMag; i++)
            {
                RaycastHit hit;
                Vector3 shootDir = bulletSpread(0.3f);
                if (Physics.Raycast(Camera.main.transform.position, shootDir, out hit, 100f, layerMask))
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        float damage = damageFalloff(hit.distance) * GameManager._.Master.weaponMaster.damageMult;
                        rayLine(hit.point);
                        hit.transform.GetComponent<EnemyHealth>().takeDamage(damage);
                    }
                    else
                    {
                        SummonWallHit(hit);
                    }
                    summonTracer(hit);
                }
            }
            PlayerController movement = GameManager._.Master.movementMaster;
            movement.KnockBack(Camera.main.transform.forward, (curMag * -5));
            curMag = 0;
            base.Special();

        }
    }
}
