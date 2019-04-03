using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PooledObject
{
    public string poolItemName = string.Empty;
    public GameObject prefab = null;
    public int poolCount = 0;
    public Transform objectParent = null;

    [SerializeField]
    private List<GameObject> poolList = new List<GameObject>();

    public void Initialize(Transform parent = null)
    {
        for (int ix = 0; ix < poolCount; ++ix)
        {
            poolList.Add(CreateItem(parent));
        }
    }

    public void PushToPool(GameObject item)
    {
        item.transform.SetParent(objectParent);
        item.SetActive(false);
        poolList.Add(item);
    }

    public GameObject PopFromPool()
    {
        if (poolList.Count == 0)
            poolList.Add(CreateItem(objectParent));

        GameObject item = poolList[0];
        poolList.RemoveAt(0);

        return item;
    }

    private GameObject CreateItem(Transform parent = null)
    {
        GameObject item = Object.Instantiate(prefab) as GameObject;
        item.name = poolItemName;
        item.transform.SetParent(parent);
        item.SetActive(false);

        return item;
    }
}