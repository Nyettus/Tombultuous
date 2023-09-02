using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitboxHandling : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        WeaponController quickRef = GameManager._.Master.weaponMaster;
        Debug.Log("Something entered melee hitbox");
        if (other.TryGetComponent(out EnemyHealth script)&& quickRef.equippedGuns[quickRef.selectedWeapon]!=null)
        {

            quickRef.equippedGuns[quickRef.selectedWeapon].OnMeleeHit(script);
        }
    }
}
