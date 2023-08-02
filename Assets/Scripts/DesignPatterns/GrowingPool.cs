using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Object pool thats good for static objects, use pooling from clicker souls if doing inheritance heavy pooling
[System.Serializable]
public class GrowingPool<T>: IPoolCountStorage where T : PooledItem
{
    //https://stackoverflow.com/questions/23519982/knowntype-attribute-cannot-use-type-parameters
    //[CustomPropClass(typeof(GrowingPool<>))] 
    // Above works but CustomPropClass only works for MonoBehaviours
    //[CustomProp]
    //public int PoolAmount { get { return available.Count; } }
    
    Queue<T> available;
    T _prefab;
    GameObject _holder;
    public GameObject Holder { get { return _holder; } }
    public GrowingPool(T prefab, int count, string poolName = "Pool")
    {
        _prefab = prefab;
        available = new Queue<T>();

        _holder = new GameObject($"{poolName} ({typeof(T)})");
        for (int i = 0; i < count; i++)
        {
            T entity = GameObject.Instantiate(prefab);
            entity.transform.SetParent(_holder.transform);
            entity.onDestroy += (x) =>
            {
                available.Enqueue(x as T);
            };
            available.Enqueue(entity);
            entity.gameObject.SetActive(false);
        }
    }

    public bool IsAvailable()
    {
        return available.Count > 0;
    }

    public T Instantiate()
    {
        return Instantiate(Vector3.zero, Quaternion.identity);
    }

    public T Instantiate(Vector3 position, Quaternion rotation)
    {
        if (available.Count <= 0)
        {
            Debug.Log($"Spare instance of {_prefab.ToString()} instantiated");
            T entity = GameObject.Instantiate(_prefab);
            entity.transform.SetParent(_holder.transform);
            entity.transform.SetPositionAndRotation(position, rotation);
            entity.onDestroy += (x) => available.Enqueue(x as T);
            entity.gameObject.SetActive(true);
            return entity;
        }

        T instantiateEntity = available.Dequeue();
        instantiateEntity.transform.SetPositionAndRotation(position, rotation);
        instantiateEntity.gameObject.SetActive(true);
        return instantiateEntity;

    }

    public int GetPoolAmount()
    {
        return available.Count;
    }

    //~GrowingPool()
    //{
    //    foreach(Transform child in _holder.transform)
    //    {
    //        GameObject.Destroy(child.gameObject);
    //    }
    //}

}

public interface IPoolCountStorage
{
    public int GetPoolAmount();
}

public abstract class PooledItem : MonoBehaviour
{

    public event System.Action<PooledItem> onDestroy;

    protected void DestroyPooled()
    {
        Reset();
        gameObject.SetActive(false);
        onDestroy?.Invoke(this);
    }

    protected abstract void Reset();
}
