using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ItemList
{
    public ItemCore item;
    public int stacks;
    public string name;


    public ItemList(ItemCore newItem, string newName, int newStacks = 1)
    {
        item = newItem;
        name = newName;
        stacks = newStacks;
    }
}
