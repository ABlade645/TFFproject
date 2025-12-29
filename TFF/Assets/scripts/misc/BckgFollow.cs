using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BckgFollow : MonoBehaviour
{
    GameObject Player;
    public float speed;
    public float height;
    public float offsetX;
    Vector2 playerPos;

    void Update()
    {
        //detect the player
        Player = GameObject.FindGameObjectWithTag("Player");

        //vector representing x axis of the player
        playerPos = new Vector2(Player.transform.position.x, height);

        //object follows the player
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
    }
}
