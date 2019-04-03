using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static Transform bulletParent;
    public static Transform effectParent;
    public static Transform monsterParent;

    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get
        {
            if (!instance)
                instance = FindObjectOfType<ObjectPool>();
            return instance;
        }
    }

    public List<PooledObject> objectPool = new List<PooledObject>();

    void Awake()
    {

        for (int i = 0; i < objectPool.Count; ++i)
        {
            objectPool[i].Initialize(objectPool[i].objectParent);
        }

    }

    public bool PushToPool(string itemName, GameObject item)
    {
        PooledObject pool = GetPoolItem(itemName);
        if (pool == null)
            return false;

        pool.PushToPool(item);
        return true;
    }

    public GameObject PopFromPool(string itemName)
    {
        PooledObject pool = GetPoolItem(itemName);
        if (pool == null)
            return null;

        return pool.PopFromPool();
    }

    PooledObject GetPoolItem(string itemName)
    {
        for (int i = 0; i < objectPool.Count; ++i)
        {
            if (objectPool[i].poolItemName.Equals(itemName))
                return objectPool[i];
        }

        Debug.LogWarning("There's no matched pool list.");
        return null;
    }
}
