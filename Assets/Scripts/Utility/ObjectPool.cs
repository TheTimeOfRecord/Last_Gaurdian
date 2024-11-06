using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : class
{
    private T prefab;
    private Queue<T> pool = new Queue<T>();
    private Transform parentTransform;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parentTransform = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T newObj = CreateNewObject();
            pool.Enqueue(newObj);
        }
    }

    private T CreateNewObject()
    {
        T newObj = GameObject.Instantiate(prefab as GameObject, parentTransform) as T;
        if (newObj is GameObject go)
            go.SetActive(false);

        return newObj;
    }

    public T Get(Vector3 position, Quaternion rotation = default)
    {
        if (pool.Count == 0)
        {
            pool.Enqueue(CreateNewObject());
        }

        T obj = pool.Dequeue();
        if (obj is GameObject go)
        {
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(true);
        }
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        if (obj is GameObject go)
        {
            go.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}