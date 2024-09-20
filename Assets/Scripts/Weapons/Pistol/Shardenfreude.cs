using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

public class Shardenfreude : HitscanWeaponBase
{

    //A shotgun of remaining shots
    public override void Special()
    {
        if (!specialUsed && curMag != 0)
        {
            specialCooldown = curMag * 0.5f;
            for (int i = 0; i < curMag; i++)
            {
                RaycastHit hit;
                Vector3 shootDir = bulletSpread(0.3f);
                if (Physics.Raycast(Camera.main.transform.position, shootDir, out hit, 100f, layerMask))
                {
                    HitscanHit(hit);
                }
            }
            PlayerController movement = GameManager._.Master.movementMaster;
            bool knockbackState = movement.rb.velocity.y < 0;
            movement.KnockBack(Camera.main.transform.forward, (curMag * -3), knockbackState);
            curMag = 0;
            SetAnimTrigger("Special");
            base.Special();

        }
    }


    public override void StartReload()
    {
        base.StartReload();
        SetAnimBool("Reload",true);
        SetAnimInt("CurMag", curMag);
        
    }
    public override void Reload()
    {
        base.Reload();
        SetAnimBool("Reload", false);
    }

    public override void Shoot()
    {
        anim.speed = 1 / GameManager._.Master.weaponMaster.hasteMult;
        base.Shoot();
        SetAnimInt("CurMag", curMag);
        SetAnimTrigger("Shoot");
    }
}
