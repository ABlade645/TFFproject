using System;
using System.Collections;
using UnityEngine;

public class PowerPointsSystem : MonoBehaviour
{
    [Header("Points")]
    public float addedPoints;
    public int points;
    public float[] addPoints;

    [Header("Multiplyer")]
    public float[] multiplyer;
    public float[] amountOfUsage;
    public float[] removeAmount;

    [Header("Timers")]    
    public float maxTimeBtwReset;
    public float[] timeBtwReset;
    bool canChangeTime = false;

    SaveSystem saveSystem;
    float setAmountOfUsage;
    StyleUI scoreboard;

    private void Start()
    {
        saveSystem = GetComponentInParent<SaveSystem>();
        points = saveSystem.playerPowerPoints;
        setAmountOfUsage = amountOfUsage[0];
        scoreboard = FindObjectOfType<StyleUI>();
    }

    //Main Data Work-----------------------------------------------------------------------------------------
    private void Update()
    {
        if (!canChangeTime)
        {
            bool canCreate = true;
            if (canCreate)
            {
                timeBtwReset = new float[amountOfUsage.Length];
                canCreate = false;
            }

            for (int i = 0; i < timeBtwReset.Length; i++)
            {
                timeBtwReset[i] = maxTimeBtwReset;
                if (i == timeBtwReset.Length - 1)
                {
                    canChangeTime = true;
                }
            }
        }
             
        //Multiplication--------------------------------------------------------------------------------------
        for (int i = 0; i < amountOfUsage.Length; i++)
        {
            switch (amountOfUsage[i])
            {
                case 10:
                    multiplyer[i] = 1.5f;
                    break;
                case 8:
                    multiplyer[i] = 1;
                    break;
                case 5:
                    multiplyer[i] = 0.5f;
                    break;
                case 0:
                    multiplyer[i] = 0;
                    break;
            }
        }

        //Timer---------------------------------------------------------------------------------------
        if (canChangeTime)
        {
            for (int i = 0; i < timeBtwReset.Length; i++)
            {
                if (timeBtwReset[i] > 0 && amountOfUsage[i] < setAmountOfUsage)
                {
                    timeBtwReset[i] -= Time.deltaTime;
                }

                if (timeBtwReset[i] <= 0)
                {
                    if (amountOfUsage[i] < setAmountOfUsage)
                    {
                        timeBtwReset[i] = maxTimeBtwReset;
                        amountOfUsage[i] += removeAmount[i];
                    }
                }
            }
        }       
    }

    //Stunts------------------------------------------------------------------------------------------
    public void Kill()
    {
        addedPoints += addPoints[0] * multiplyer[0];
        amountOfUsage[0] -= removeAmount[0];
        timeBtwReset[0] = maxTimeBtwReset;
        Debug.Log("stunt: Kill " + addPoints[0] * multiplyer[0]);
        scoreboard.InvokeStyleMeter("KILL");
    }

    public void Stoned()
    {
        addedPoints += addPoints[1] * multiplyer[1];
        amountOfUsage[1] -= removeAmount[1];
        timeBtwReset[1] = maxTimeBtwReset;
        Debug.Log("stunt: Stoned " + addPoints[1] * multiplyer[1]);
        scoreboard.InvokeStyleMeter("STONED");
    }

    public void Airborn()
    {
        addedPoints += addPoints[2] * multiplyer[2];
        amountOfUsage[2] -= removeAmount[2];
        timeBtwReset[2] = maxTimeBtwReset;
        Debug.Log("stunt: Airborn " + addPoints[2] * multiplyer[2]);
        scoreboard.InvokeStyleMeter("AIRBORN");
    }

    public void Sniped()
    {
        addedPoints += addPoints[3] * multiplyer[3];
        amountOfUsage[3] -= removeAmount[3];
        timeBtwReset[3] = maxTimeBtwReset;
        Debug.Log("stunt: Sniped " + addPoints[3] * multiplyer[3]);
        scoreboard.InvokeStyleMeter("SNIPED");
    }

    //saving--------------------------------------------------
    public void Save()
    {
        points += (int)addedPoints;
        addedPoints = 0;
        saveSystem.playerPowerPoints = points;
    }
}
