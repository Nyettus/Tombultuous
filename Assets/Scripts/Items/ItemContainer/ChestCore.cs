using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

public class ChestCore : MonoBehaviour
{
    public ItemPools itemCard;
    public WeaponPool weaponCard;
    public Transform location;

    private ItemBase itemToSpawn;
    private WeaponBase weaponToSpawn;
    private GameObject itemToInstant = null;


    [SerializeField] private GameObject CustomSpawn;
    public bool CustomItem = false;



    // Start is called before the first frame update
    void Start()
    {
        if (itemCard != null) itemToSpawn = itemCard.ReturnItem(itemCard.chestChance);
        if (weaponCard != null) weaponToSpawn = weaponCard.ReturnWeapon();

    }

    private bool once = true;
    private GameObject SelectItem()
    {
        if (!once) return null;
        if (CustomItem && once)
        {
            once = false;
            return CustomSpawn;
        }
        else
        {
            float chance = Random.value;
            if (chance <= WeaponChance())
                itemToInstant = weaponToSpawn.prefab;
            else
                itemToInstant = itemToSpawn.prefab;

            once = false;
        }
        return itemToInstant;

    }

    //Change chance of weapon drop
    private float WeaponChance()
    {
        var holding = GameManager._.Master.weaponMaster.equippedGuns;
        int numberOfWeapon = 0;
        foreach (var weapon in holding)
        {
            if (weapon == null) continue;
            numberOfWeapon++;
        }
        if (numberOfWeapon == holding.Length)
            return 0.3f;
        else
            return 0.8f;
    }



    public void SpawnItem()
    {
        var objectToSpawn = SelectItem();
        if (objectToSpawn == null) return;
        Instantiate(objectToSpawn, location.position, location.rotation);

    }
}
