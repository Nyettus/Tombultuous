using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : HitscanWeaponBase
{
    public override void Special()
    {

        if (!specialUsed)
        {
            RaycastHit hit;
            Vector3 shootDir = bulletSpread(spread);
            if (Physics.Raycast(Camera.main.transform.position, shootDir, out hit, 100f, layerMask))
            {
                Vector3 pos = Vector3.Lerp(GameManager._.Master.transform.position, hit.point, 0.9f);
                GameManager._.Master.movementMaster.rb.MovePosition(pos);
                if (hit.transform.tag == "Enemy")
                {
                    float damage = damageFalloff(hit.distance) * GameManager._.Master.weaponMaster.damageMult*2f;
                    hit.transform.GetComponent<EnemyHealth>().takeDamage(damage);

                }
                GameManager._.Master.cameraEffects.DashShake(10f);
                base.Special();
            }


        }
    }
}
