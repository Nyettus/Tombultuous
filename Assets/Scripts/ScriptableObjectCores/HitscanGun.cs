using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Hitscan",menuName = "Weapons/Ranged/Hitscan")]
public class HitscanGun : WeaponBase
{
    [Header("Basic Weapon Traits")]
    public float maxDamage;
    public float minDamage;
    public int bullets;
    public float fireRate;
    public float spread;
    public bool fullAuto;

    [Header("Fall Off")]
    public float minRange;
    public float maxRange;

    [Header("Reloadable Traits")]
    public bool requireReload;
    public int magSize;
    public float reloadTime;

    [Header("Special Move")]
    public float cooldown;


}
