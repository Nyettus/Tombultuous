using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PoolTiers
{
    Tier1,
    Tier2
}


[CreateAssetMenu(fileName = "new Object Pool", menuName = "Pools/Item Pool")]
public class ItemPools : ScriptableObject
{

    public ItemBase[] tier1;
    public ItemBase[] tier2;
    

}
