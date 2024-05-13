using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitboxHandling : MonoBehaviour
{
    public List<IEnemyDamageable> previousHits = new List<IEnemyDamageable>();

    private void OnTriggerEnter(Collider other)
    {
        
        WeaponController quickRef = GameManager._.Master.weaponMaster;
        if (quickRef.equippedGuns[quickRef.selectedWeapon] == null) return;
        if(other.TryGetComponent(out IEnemyDamageable hitbox))
        {
            if (!previousHits.Contains(hitbox))
            {
                previousHits.Add(hitbox);
                quickRef.equippedGuns[quickRef.selectedWeapon].OnMeleeHit(hitbox);
            }
            
        }
        else if(other.TryGetComponent(out ChestCore chest))
        {
            chest.SpawnItem();
        }
        else if(other.tag == "MasterTomb")
        {
            other.transform.parent.GetComponent<MasterTomb>().DestroySelf();
        }
        else if (other.TryGetComponent(out NextLevel level))
        {
            level.GotoNextLevel();
        }



    }
}
