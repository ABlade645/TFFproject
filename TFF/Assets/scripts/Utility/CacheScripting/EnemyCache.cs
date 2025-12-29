using System.Collections.Generic;
using UnityEngine;

public class EnemyCache : MonoBehaviour
{
    public bool cacheObjects;
    bool canCacheObjects;
    bool beginCaching;
    public GameObject[] enemiesToCache;
    public string[] enemyNames;
    public Dictionary<string, GameObject> enemyCache = new Dictionary<string, GameObject>();

    void Start()
    {
        enemyNames = new string[enemiesToCache.Length];
    }

    void Update()
    {
        if (cacheObjects && !canCacheObjects)
        {
            canCacheObjects = true;
        }

        if (canCacheObjects && enemyCache.Count != enemiesToCache.Length)
        {           
            for (int i = 0; i < enemyNames.Length; i++)
            {
                if (enemyNames[i] != enemiesToCache[i].name)
                {
                    enemyNames[i] = enemiesToCache[i].name;
                }

                if (i == enemyNames.Length - 1)
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
            Debug.Log(enemyCache);
        }
    }

    public void AddToCache()
    {
        for (int i = 0; i < enemiesToCache.Length; i++)
        {
            if (!enemyCache.ContainsKey(enemyNames[i]))
            {
                enemyCache[enemyNames[i]] = enemiesToCache[i];
            }
            if (i == enemiesToCache.Length - 1)
            {
                beginCaching = false;
            }
        }      
    }

    public GameObject GetFromCache(string key)
    {
        if (enemyCache.ContainsKey(key))
        {
            return enemyCache[key];
        }
        else
        {
            Debug.LogError("Cache error: Key \""+key+"\" not found");
            return null;
        }
    }
}
