using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

public class ChestCore : MonoBehaviour
{
    public ItemPools itemCard;
    public WeaponPool weaponCard;
    public Transform location;

    public PoolTiers whichTier;
    [SerializeField]
    private ItemBase itemToSpawn;
    [SerializeField]
    private WeaponBase weaponToSpawn;

    private float weaponPercent = 0.50f;



    // Start is called before the first frame update
    void Start()
    {

        itemToSpawn = MoneySack.FindItem(itemCard);
        weaponToSpawn = MoneySack.FindWeapon(weaponCard);

    }

    // Update is called once per frame
    void Update()
    {

    }

    


    private bool once = true;
    public void SpawnItem()
    {
        GameObject itemToInstant = null;
        if (once)
        {
            float chance = Random.value;
            if (chance <= weaponPercent)
                itemToInstant = weaponToSpawn.prefab;
            else
                itemToInstant = itemToSpawn.prefab;
            Instantiate(itemToInstant, location.position, location.rotation);
            once = false;
        }

    }
}
