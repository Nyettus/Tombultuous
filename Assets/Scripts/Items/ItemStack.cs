using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ItemStack
{
    public ItemBase item;
    public int stacks;


    public ItemStack(ItemBase newItem, int newStacks = 1)
    {
        item = newItem;
        stacks = newStacks;
    }
}
