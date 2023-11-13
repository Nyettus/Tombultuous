using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WeaponStorage
{
    public GameObject weaponPrefab;
    public float specialRemaining;
    public int ammoRemaining;

    public WeaponStorage(GameObject weapon,float remainingSpecial, int ammo)
    {
        weaponPrefab = weapon;
        specialRemaining = remainingSpecial;
        ammoRemaining = ammo;
    }

}
