using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;

public class ObjectPooler : MonoBehaviour
{


    public ObjectPoolerPool totalPoolableObjects;
    public static ObjectPooler Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot)
    {

        AddToPool(totalPoolableObjects.findPooledItem(tag));
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(false);
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rot;

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;

    }


    public void AddToPool(Pool pooledItem)
    {
        if (poolDictionary.ContainsKey(pooledItem.tag)) return;
        Queue<GameObject> objectPool = new Queue<GameObject>();

        for (int i = 0; i < pooledItem.size; i++)
        {
            GameObject obj = Instantiate(pooledItem.prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        poolDictionary.Add(pooledItem.tag, objectPool);
    }


}
