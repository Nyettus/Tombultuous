using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    public PlayableCharacter character;
    private PlayerMaster holder => GameManager._.Master;

    public void EquipNewCharacter()
    {
        
        if (character == holder.card) return;
        GameManager._.playerCard= character;
        holder.EstablishCard();
        GiveItems();
        StartCoroutine("GiveWeapon");
    }

    private void GiveItems()
    {
        holder.itemMaster.CleanseItems();
        foreach (ItemBase item in character.startingItems)
        {
            holder.itemMaster.GetItem(item);
        }
    }

    private IEnumerator GiveWeapon()
    {
        holder.weaponMaster.CleanseWeapons();
        foreach (WeaponBase weapon in character.startingWeapons)
        {
            var currentWep = Instantiate(weapon.prefab);
            Debug.Log("I spawned a weapon");
            var currentCore = currentWep.GetComponent<WeaponCore>();
            yield return new WaitForSeconds(0.1f);
            currentCore.pickUpWeapon();
        }

    }
}
