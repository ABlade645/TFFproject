using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointLevel : MonoBehaviour
{
    public GameObject player;
    public int checkpointIndex;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Awake();
        }
    }

    private void Awake()
    {
        if (player.GetComponent<playercontroller>().storedCheckPointIndex == checkpointIndex)
        {
            player.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<playercontroller>().storedCheckPointIndex++;
            Deactivate();
        }
    }

    void Deactivate()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
