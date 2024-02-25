using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Melee", menuName = "Weapons/Melee/Standard")]
public class MeleeWeapon : WeaponBase
{
    [Header("Melee Characteristics")]
    public float damage;
    public float swingSpeed;
    public float hitBoxLength;
    public float hitboxWidth;
}