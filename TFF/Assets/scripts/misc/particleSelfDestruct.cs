
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSelfDestruct : MonoBehaviour
{
    public float maxTimeUntillDeath;
    float timeuntillDeath;

    bool isSpawned = true;

    void Update()
    {
        if (isSpawned)
        {
            timeuntillDeath = maxTimeUntillDeath;
            isSpawned = false;
        }

        if (timeuntillDeath > 0)
        {
            timeuntillDeath -= Time.deltaTime;
        }

        if (timeuntillDeath <= 0)
        {
            Destroy(gameObject);
        }
    }
}
