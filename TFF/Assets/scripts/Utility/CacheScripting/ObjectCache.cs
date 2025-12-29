using System.Collections.Generic;
using UnityEngine;

public class ObjectCache : MonoBehaviour
{
    public bool cacheObjects;
    bool canCacheObjects;
    bool beginCaching;
    public GameObject[] objectsToCache;
    public string[] objectNames;
    public Dictionary<string, GameObject> objectCache = new Dictionary<string, GameObject>();

    void Start()
    {
        objectNames = new string[objectsToCache.Length];
    }

    void Update()
    {
        if (cacheObjects && !canCacheObjects)
        {
            canCacheObjects = true;
        }

        if (canCacheObjects && objectCache.Count != objectsToCache.Length)
        {
            for (int i = 0; i < objectNames.Length; i++)
            {
                if (objectNames[i] != objectsToCache[i].name)
                {
                    objectNames[i] = objectsToCache[i].name;
                }

                if (i == objectNames.Length - 1)
                {
                    beginCaching = true;
                }
            }
            if (beginCaching)
            {
                AddToCache();
            }

        }
        else
        {
            canCacheObjects = false;
        }

        if (Input.GetKeyDown(KeyCode.Tilde))
        {
            Debug.Log(objectCache);
        }
    }

    public void AddToCache()
    {
        for (int i = 0; i < objectsToCache.Length; i++)
        {
            if (!objectCache.ContainsKey(objectNames[i]))
            {
                objectCache[objectNames[i]] = objectsToCache[i];
            }
            if (i == objectsToCache.Length - 1)
            {
                beginCaching = false;
            }
        }
    }

    public GameObject GetFromCache(string key)
    {
        if (objectCache.ContainsKey(key))
        {
            return objectCache[key];
        }
        else
        {
            Debug.LogError("Cache error: Key \"" + key + "\" not found");
            return null;
        }
    }
}
