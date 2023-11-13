using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

[CreateAssetMenu(fileName = "ItemBase", menuName = "Items/BasicItem")]
public class ItemBase : ScriptableObject
{
  
    public string itemName;
    public string itemDesc;
    public GameObject prefab;

}
