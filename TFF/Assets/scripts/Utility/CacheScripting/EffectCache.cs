using System.Collections.Generic;
using UnityEngine;

public class EffectCache : MonoBehaviour
{
    public bool cacheObjects;
    bool canCacheObjects;
    bool beginCaching;
    public GameObject[] effectsToCache;
    public string[] effectNames;
    public Dictionary<string, GameObject> effectCache = new Dictionary<string, GameObject>();

    void Start()
    {
        effectNames = new string[effectsToCache.Length];
    }

    void Update()
    {
        if (cacheObjects && !canCacheObjects)
        {
            canCacheObjects = true;
        }

        if (canCacheObjects && effectCache.Count != effectsToCache.Length)
        {
            for (int i = 0; i < effectNames.Length; i++)
            {
                if (effectNames[i] != effectsToCache[i].name)
                {
                    effectNames[i] = effectsToCache[i].name;
                }

                if (i == effectNames.Length - 1)
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
            Debug.Log(effectCache);
        }
    }

    public void AddToCache()
    {
        for (int i = 0; i < effectsToCache.Length; i++)
        {
            if (!effectCache.ContainsKey(effectNames[i]))
            {
                effectCache[effectNames[i]] = effectsToCache[i];
            }
            if (i == effectsToCache.Length - 1)
            {
                beginCaching = false;
            }
        }
    }

    public GameObject GetFromCache(string key)
    {
        if (effectCache.ContainsKey(key))
        {
            return effectCache[key];
        }
        else
        {
            Debug.LogError("Cache error: Key \"" + key + "\" not found");
            return null;
        }
    }
}
