using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekaAttack : MonoBehaviour
{
    public string projectileName;
    public ParticleSystem chargeParticle;
    public Transform pos;

    public float maxTimeBtwAttack;
    float bufferTimeBtwAttack;
    float timeBtwAttack;

    bool canCharge = true;

    StatusEffects status;
    bool canBePoisoned = true;

    bool isSpawned = true;
    ObjectCache cache;

    private void Update()
    {
        if (isSpawned)
        {
            cache = GameObject.FindGameObjectWithTag("ObjectCache").GetComponent<ObjectCache>();
            isSpawned = false;
        }

        if (status == null)
        {
            status = GetComponent<StatusEffects>();
            bufferTimeBtwAttack = maxTimeBtwAttack;
        }

        if (timeBtwAttack > 0)
        {
            timeBtwAttack -= Time.deltaTime;
            if (timeBtwAttack < bufferTimeBtwAttack/2 && canCharge == true)
            {
                canCharge = false;
                chargeParticle.Play();
            }
        }

        if (timeBtwAttack <= 0)
        {
            canCharge = true;
            timeBtwAttack = bufferTimeBtwAttack;
            Instantiate(cache.GetFromCache(projectileName), pos.position, Quaternion.identity);
        }

        if (status.poison && canBePoisoned)
        {
            canBePoisoned = false;
            bufferTimeBtwAttack /= 2;
        }

        if (status.poison == false && canBePoisoned == false)
        {
            canBePoisoned = true;
        }
    }
}
