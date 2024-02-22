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

    private float weaponPercent = 0.50f;

    [SerializeField] private GameObject CustomSpawn;
    public bool CustomItem = false;



    // Start is called before the first frame update
    void Start()
    {
        if(itemCard!=null) itemToSpawn = GunBox.FindItem(itemCard);
        if(weaponCard!=null)weaponToSpawn = GunBox.FindWeapon(weaponCard);

    }

    private bool once = true;
    private GameObject SelectItem()
    {
        if (CustomItem)
        {
            return CustomSpawn;
        }
        else if (once)
        {
            float chance = Random.value;
            if (chance <= weaponPercent)
                itemToInstant = weaponToSpawn.prefab;
            else
                itemToInstant = itemToSpawn.prefab;

            once = false;
        }
        return itemToInstant;

    }





    public void SpawnItem()
    {

        Instantiate(SelectItem(), location.position, location.rotation);

    }
}
