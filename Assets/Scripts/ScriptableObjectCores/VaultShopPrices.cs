using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new VaultPrices",menuName = "Shop/VaultPrices") ]
public class VaultShopPrices : ScriptableObject
{
    public int recyclePrice;
    public int[] healingPrice;

}
