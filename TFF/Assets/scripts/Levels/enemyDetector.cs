using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDetector : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] triggers;
    public bool defeated;
    public bool canDefeat;
    public int currentEncounter;

    private void Start()
    {
        currentEncounter = -1;
        triggers = GameObject.FindGameObjectsWithTag("Trigger");
    }

    private void Awake()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);

            if (i == enemies.Length)
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
            }
        }

        for (int i = 0; i < currentEncounter; i++)
        {
            triggers[i].GetComponent<enemyInstaciationTrigger>().canInstanciate = false;
        }
    }

    void Update()
    {
        if (enemies.Length == 0 && canDefeat == true)
        {
            canDefeat = false;
            defeated = true;
            currentEncounter += 1;
        }

        Invoke("Check", 5f);
    }

    public void Spawned()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        canDefeat = true;
        defeated = false;
    }

    public void Check()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
}
