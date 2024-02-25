using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Hitscan", menuName = "Weapons/Ranged/HitscanGun")]
public class HitscanGun : RangedGunBase
{
    [Header("Fall Off")]
    public float maxDamage;
    public float minDamage;
    public float minRange;
    public float maxRange;
}
