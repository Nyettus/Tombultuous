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
                if (Physics.Raycast(Camera.main.transform.position, shootDir, out hit, 100f, ~(1 << 6)))
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        float damage = damageFalloff(hit.distance) * GameManager.instance.Master.damage;
                        rayLine(hit.point);
                        hit.transform.GetComponent<EnemyHealth>().takeDamage(damage);
                    }
                    GameObject tracer = ObjectPool.SpawnFromPool("Tracer", Camera.main.transform.position, transform.rotation);
                    if (tracer.TryGetComponent(out HitscanTracers script))
                    {
                        script.EstablishTrails(transform.position, hit.point);
                    }
                }
            }
            PlayerController movement = GameManager.instance.Master.movementMaster;
            movement.rb.velocity = Vector3.zero;
            movement.rb.AddForce(Camera.main.transform.forward * (curMag*-5),ForceMode.Impulse);
            curMag = 0;
            base.Special();
            StartCoroutine(startSpecialCooldown());
        }
    }
}
