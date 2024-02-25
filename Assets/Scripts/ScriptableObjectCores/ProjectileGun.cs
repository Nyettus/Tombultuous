using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Hitscan", menuName = "Weapons/Ranged/ProjectileGun")]
public class ProjectileGun : WeaponBase
{
    [Header("Basic Weapon Traits")]
    public int bullets;
    public float fireRate;
    public float spread;
    public bool fullAuto;


    [Header("Reloadable Traits")]
    public bool requireReload;
    public int magSize;
    public float reloadTime;


}
