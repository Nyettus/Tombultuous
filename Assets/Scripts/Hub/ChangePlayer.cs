using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    public PlayableCharacter character;

    public void EquipNewCharacter()
    {
        PlayerMaster holder = GameManager._.Master;
        holder.card = character;
        holder.EstablishCard();
        holder.itemMaster.CleanseItems();
        foreach(ItemBase item in character.startingItems)
        {
            holder.itemMaster.GetItem(item);
        }
        holder.weaponMaster.CleanseWeapons();
        StartCoroutine("GiveWeapon");
    }

    private IEnumerator GiveWeapon()
    {
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
