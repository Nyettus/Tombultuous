using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

[CreateAssetMenu(fileName = "ItemBase", menuName = "Items/BasicItem")]
public class ItemBase : ScriptableObject
{
    [ScriptableObjectId]
    public string ID;
    public bool unlocked = true;

    public string itemName;
    public string itemLore;
    public string itemDesc;

    public GameObject prefab;

    public int tier;

}
