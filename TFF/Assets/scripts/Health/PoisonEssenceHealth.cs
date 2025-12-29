using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEssenceHealth : MonoBehaviour, IDamagable
{
    public float health;
    public float maxHealth = 20;

    public float DMultP;
    public float DMultR;

    bool isSpawned = true;

    bool killedByPhys;
    PowerPointsSystem points;

    private void Update()
    {
        if (isSpawned)
        {
            isSpawned = false;
            Checkup();
        }

        if (health <= 0)
        {
            if (killedByPhys)
            {
                points.Kill();
            }

            Destroy(gameObject);
        }
    }

    public void TakeDamagePhysical(float damage)
    {
        health -= damage * DMultP;
        if (health <= 0)
        {
            killedByPhys = true;
        }
    }

    public void TakeDamageRanged(float damage)
    {
        health -= damage * DMultR;
    }

    void Checkup()
    {
        health = maxHealth;
        points = GameObject.Find("ScoreManager").GetComponent<PowerPointsSystem>();
    }
}
