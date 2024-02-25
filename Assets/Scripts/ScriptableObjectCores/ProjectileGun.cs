using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ProjectileGun", menuName = "Weapons/Ranged/ProjectileGun")]
public class ProjectileGun : RangedGunBase
{
    [Header("Projectile")]
    public ProjectileType projectile;


}
