using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZZTHealth : MonoBehaviour, IDamagable
{
    public int maxHealth;
    public float health;
    public float multR;
    public float multP;

    public Image bar;
    public Image delayBar;

    public float delayTime;
    public float maxDelayTime;
    public float delaySpeed;

    bool isSpawned = true;

    private void Update()
    {
        if (isSpawned)
        {
            health = maxHealth;
            isSpawned = false;
        }

        float barAmount = health / maxHealth;

        bar.fillAmount = barAmount;

        if (delayBar.fillAmount != bar.fillAmount && delayTime != maxDelayTime)
        {
            delayTime = maxDelayTime;
        }

        if (delayTime > 0)
        {
            delayTime -= Time.deltaTime;
        }

        if (delayTime <= 0)
        {
            delayBar.fillAmount -= (Time.deltaTime * delaySpeed) / 100;
        }
    }

    public void TakeDamagePhysical(float damage)
    {
        health -= damage * multP;
    }

    public void TakeDamageRanged(float damage)
    {
        health -= damage * multR;
    }
}
