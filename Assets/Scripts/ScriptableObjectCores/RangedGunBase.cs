using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangedGunBase : WeaponBase
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
