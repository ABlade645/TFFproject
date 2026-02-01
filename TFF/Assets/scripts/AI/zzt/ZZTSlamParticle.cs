using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZTSlamParticle : MonoBehaviour
{
    bool isSpawned = true;

    // Update is called once per frame
    void Update()
    {
        if (isSpawned)
        {
            GetComponent<ParticleSystem>().Play();
            Invoke("selfDestruct", 1);
            isSpawned = false;
        }
    }

    void selfDestruct()
    {
        Destroy(gameObject);
    }
}
