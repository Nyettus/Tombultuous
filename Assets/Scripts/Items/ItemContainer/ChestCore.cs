using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCore : MonoBehaviour
{
    public ItemPools itemCard;
    public WeaponPool weaponCard;
    public Transform location;

    public PoolTiers whichTier;
    private ItemBase itemToSpawn;

    public WeaponBase weaponToSpawn;

    private float weaponPercent = 0.50f;



    // Start is called before the first frame update
    void Start()
    {

        FindItem();
        FindWeapon();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FindItem()
    {
        whichTier = ReturnTier();

        int poolLength;
        int whichItem;
        ItemBase[] tierPool = null;

        switch (whichTier)
        {
            case PoolTiers.Tier1:
                tierPool = itemCard.tier1;

                break;
            case PoolTiers.Tier2:
                tierPool = itemCard.tier2;
                break;
            default:
                Debug.LogError("Invalid Tier");
                break;
        }

        if (tierPool != null)
        {
            poolLength = tierPool.Length;
            whichItem = Random.Range(0, poolLength);
            itemToSpawn = tierPool[whichItem];
        }
        else Debug.LogError("Could not find item to spawn");

    }

    private PoolTiers ReturnTier()
    {
        float random = Random.value;
        if (random < 0.7) return PoolTiers.Tier1;
        else return PoolTiers.Tier2;
    }

    private void FindWeapon()
    {
        int value = Random.Range(0, weaponCard.tier1.Length);
        weaponToSpawn = weaponCard.tier1[value];

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
