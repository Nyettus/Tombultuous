using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Weapon Pool", menuName = "Pools/Weapon Pool")]
public class WeaponPool : ScriptableObject
{

    public WeaponBase[] tier1;

    public WeaponBase ReturnWeapon()
    {
        int randomIndex = Random.Range(0, tier1.Length);
        return tier1[randomIndex];
    }

}

