using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolPrewarmer : MonoBehaviour
{
    [SerializeField] private string[] poolTags;

    // Start is called before the first frame update
    void Start()
    {
        SpawnFromPool();
    }

    private void SpawnFromPool()
    {
        foreach(string name in poolTags)
        {
            ObjectPooler._.SpawnFromPool(name, this.transform.position,Quaternion.identity);
        }
    }

}
