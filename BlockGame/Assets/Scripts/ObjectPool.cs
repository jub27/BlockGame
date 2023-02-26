using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private List<T> pooledObjects = new List<T>();
    private T prefab;
    private int maxObjects;

    public ObjectPool(T prefab, int maxObjects)
    {
        this.prefab = prefab;
        this.maxObjects = maxObjects;
    }

    // 오브젝트를 풀에서 가져오거나, 생성해서 반환합니다.
    public T GetObject()
    {
        foreach (T obj in pooledObjects)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        if (pooledObjects.Count < maxObjects)
        {
            T newObj = Object.Instantiate(prefab);
            pooledObjects.Add(newObj);
            newObj.gameObject.SetActive(true);
            return newObj;
        }

        return null;
    }

    // 오브젝트를 비활성화해서 풀에 반환합니다.
    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}