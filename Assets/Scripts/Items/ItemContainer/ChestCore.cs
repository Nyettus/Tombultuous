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
        if (!once) return null;
        if (CustomItem && once)
        {
            once = false;
            return CustomSpawn;
        }
        else
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
        var objectToSpawn = SelectItem();
        if (objectToSpawn == null) return;
        Instantiate(objectToSpawn, location.position, location.rotation);

    }
}
