using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOnDeath : MonoBehaviour
{
    PlayerHealth player;
    SaveSystem saveSystem;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        saveSystem = GameObject.Find("SaveSystem").GetComponent<SaveSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health <= 0)
        {
            saveSystem.SaveIndex();
        }
    }
}
