using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomInstanciator : MonoBehaviour
{
    int random;
    EnemyCache enemyCache;

    void Start()
    {
        enemyCache = GameObject.FindGameObjectWithTag("EnemyCache").GetComponent<EnemyCache>();
    }

    private void Update()
    {
        if (enemyCache != null) 
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                random = Random.Range(0, enemyCache.enemyNames.Length);
                Instantiate(enemyCache.GetFromCache(enemyCache.enemyNames[random]), transform.position, Quaternion.identity);
            }
        }       
    }
}
