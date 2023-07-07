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
            if (Physics.Raycast(Camera.main.transform.position, shootDir, out hit, 100f, ~(1 << 6)))
            {
                Vector3 pos = Vector3.Lerp(GameManager.instance.Master.transform.position, hit.point, 0.9f);
                GameManager.instance.Master.movementMaster.rb.MovePosition(pos);
                if (hit.transform.tag == "Enemy")
                {
                    float damage = damageFalloff(hit.distance) * GameManager.instance.Master.weaponMaster.damageMult*2f;
                    hit.transform.GetComponent<EnemyHealth>().takeDamage(damage);
                    StartCoroutine(GameManager.instance.Master.cameraEffects.DashShake(0.1f, 0.1f));
                }
            }
          base.Special();
        }
    }
}
