using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeExpl : MonoBehaviour
{
    public GameObject particle;

    bool isSpawned = true;

    void Update()
    {
        if (isSpawned)
        {
            particle.GetComponent<ParticleSystem>().Play();
            isSpawned = false;

            Invoke("Delete", 2.5f);
        }
    }

    void Delete()
    {
        Destroy(gameObject);
    }
}
