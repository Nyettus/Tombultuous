using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitboxHandling : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        WeaponController quickRef = GameManager._.Master.weaponMaster;
        if (quickRef.equippedGuns[quickRef.selectedWeapon] == null) return;
        if (other.TryGetComponent(out EnemyHealth script))
        {
            quickRef.equippedGuns[quickRef.selectedWeapon].OnMeleeHit(script);
            GameManager._.Master.itemMaster.onHitEffectHandler.OnHitEffect(script.transform.position);
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
        else
        {
            Debug.Log("Nothing hit");
        }


    }
}
