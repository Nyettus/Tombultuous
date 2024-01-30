using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCore : MonoBehaviour
{
    public ItemPools card;
    public Transform location;

    public PoolTiers whichTier;
    private ItemBase itemToSpawn;


    // Start is called before the first frame update
    void Start()
    {
        FindItem();

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
                tierPool = card.tier1;

                break;
            case PoolTiers.Tier2:
                tierPool = card.tier2;
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

    private bool once = true;
    public void SpawnItem()
    {
        if (once)
        {
            Instantiate(itemToSpawn.prefab, location.position, location.rotation);
            once = false;
        }

    }
}
