using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item Shop Entry", menuName = "Shop/ItemShopEntry")]
public class ItemShopEntry : ScriptableObject
{
    public ItemBase itemToUnlock;
    public int goldCost;
    public int scrapCost;
}
