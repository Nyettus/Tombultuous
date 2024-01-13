using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

[CreateAssetMenu(fileName = "new Object Pool", menuName = "Pools/Object Pool")]

public class ObjectPoolerPool : ScriptableObject
{
    public Pool[] poolableItems;

    public Pool findPooledItem(string tag)
    {
        foreach(Pool pool in poolableItems)
        {
            if (pool.tag == tag) return pool;
        }
        Debug.LogError("Pooled item does not exist");
        return null;
    }

}
