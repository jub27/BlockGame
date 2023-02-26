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

    // ������Ʈ�� Ǯ���� �������ų�, �����ؼ� ��ȯ�մϴ�.
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

    // ������Ʈ�� ��Ȱ��ȭ�ؼ� Ǯ�� ��ȯ�մϴ�.
    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}