using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public enum PoolTiers
{
    Tier1,
    Tier2,
    Tier3,
    Tier4
}


[CreateAssetMenu(fileName = "new Object Pool", menuName = "Pools/Item Pool")]
public class ItemPools : ScriptableObject
{
    public Gradient defaultChance;
    public Gradient chestChance;
    public float[] tierThresholds = { 0f, 0.2f, 0.4f, 0.6f };

    public ItemBase[] tier1;
    public ItemBase[] tier2;
    public ItemBase[] tier3;
    public ItemBase[] tier4;


    //Red value determines rarity
    private PoolTiers ReturnTier(Gradient dropChances)
    {
        float r = Random.value;
        float rVal = dropChances.Evaluate(r).r;

        for (int i = 0; i < tierThresholds.Length; i++)
        {
            if (rVal == tierThresholds[i])
                return (PoolTiers)i;
        }
        Debug.LogError("Invalid tier");
        return PoolTiers.Tier1;
    }

    private ItemBase[] ReturnArray(Gradient dropChances)
    {
        switch (ReturnTier(dropChances))
        {
            case PoolTiers.Tier1:
                return tier1;
            case PoolTiers.Tier2:
                return tier2;
            case PoolTiers.Tier3:
                return tier3;
            case PoolTiers.Tier4:
                return tier4;
            default:
                Debug.LogError("Invalid tier");
                return null;

        }
    }

    public ItemBase ReturnItem(Gradient dropChances)
    {
        var whichPool = ReturnArray(dropChances);
        var filteredArray = whichPool.Where(item => item.unlocked).ToArray();
        int randomIndex = Random.Range(0, filteredArray.Length);
        return filteredArray[randomIndex];
    }



}
