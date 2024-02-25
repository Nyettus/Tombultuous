using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBase : RangedWeaponBase
{
    public override void Shoot()
    {
        base.Shoot();

    }

    protected void LaunchProjectile()
    {
        GameObject projectile = ObjectPool.SpawnFromPool("TestProj",Camera.main.transform.position, transform.rotation);
    }

}
