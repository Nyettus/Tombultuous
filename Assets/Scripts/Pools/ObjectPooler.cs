using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulBox;
using UnityEngine.SceneManagement;

public class ObjectPooler : SingletonPersist<ObjectPooler>
{


    public ObjectPoolerPool totalPoolableObjects;

    private void Awake()
    {
        Startup(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
        pools.Add(pooledItem);
        //for (int i = 0; i < pooledItem.size; i++)
        //{
        //    GameObject obj = Instantiate(pooledItem.prefab);
        //    obj.SetActive(false);
        //    objectPool.Enqueue(obj);
        //}
        StartCoroutine( slowSpawn(objectPool, pooledItem));

        poolDictionary.Add(pooledItem.tag, objectPool);
    }

    private IEnumerator slowSpawn(Queue<GameObject> objectpool,Pool pooledItem)
    {
        do
        {
            GameObject obj = Instantiate(pooledItem.prefab);
            obj.SetActive(false);
            objectpool.Enqueue(obj);
            yield return new WaitForSeconds(0.25f);
        } while (objectpool.Count < pooledItem.size);
    }


}
