using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBase : RangedWeaponBase
{
    public override void Shoot()
    {
        base.Shoot();
        LaunchProjectile();
    }

    protected void LaunchProjectile()
    {
        Vector3 position = Camera.main.transform.position + transform.forward * 2;
        GameObject projectile = ObjectPool.SpawnFromPool("TestProj",position, transform.rotation);
        projectile.GetComponent<ProjectileManager>().Initialise(position, transform.rotation);
    }

}
