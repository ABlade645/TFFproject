using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antiWallClip : MonoBehaviour
{
    GameObject player;
    public float heigth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + heigth);
        }
    }
}
