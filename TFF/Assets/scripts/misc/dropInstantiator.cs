using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropInstantiator : MonoBehaviour
{
    bool canDrop = true;
    bool dropPhase;
    public string dropName;
    public int dropAmount;
    public int amountOffset;
    int dropValue;

    bool isSpawned = true;
    ObjectCache cache;

    void Update()
    {
        if (isSpawned)
        {
            cache = GameObject.FindGameObjectWithTag("ObjectCache").GetComponent<ObjectCache>();
            isSpawned = false;
        }

        if (canDrop == true)
        {
            dropValue = Random.Range(dropAmount - amountOffset, dropAmount + (1 + amountOffset));
            if (dropValue < 0)
            {
                dropValue = 0;
            }
            dropPhase = true;
        }

        if (dropPhase == true)
        {
            for (int i = 0; i < dropValue; i++)
            {
                Instantiate(cache.GetFromCache(dropName), transform.position, Quaternion.identity);
                if (i == dropValue - 1)
                {
                    Destroy(gameObject);
                    dropPhase = false;
                }
            }
        }
    }
}
