using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyInstanciator : MonoBehaviour
{
    EnemyCache cache;
    public string enemyName;

    void Start()
    {
        cache = GameObject.FindGameObjectWithTag("EnemyCache").GetComponent<EnemyCache>();
    }

    public void Instanciate()
    {
        Instantiate(cache.GetFromCache(enemyName), transform.position, Quaternion.identity);
    }
}
