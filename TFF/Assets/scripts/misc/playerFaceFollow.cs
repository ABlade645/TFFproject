using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFaceFollow : MonoBehaviour
{
    GameObject player;
    GameObject cursor;
    public float offsetX;
    public float offsetY;

    float speed;
    public float baseSpeed;
    public float speedMultiplier;
    int difference;

    void Start()
    {
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player.transform.position.x > cursor.transform.position.x)
        {
            difference = -1;
            offsetX = 0.3f;
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (player.transform.position.x < cursor.transform.position.x)
        {
            difference = 1;
            offsetX = 0.15f;
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (transform.position != player.transform.position)
        {
            speed = baseSpeed * (speedMultiplier * Vector2.Distance(transform.position, player.transform.position + new Vector3(offsetX * difference, offsetY, player.transform.position.z)));
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position + new Vector3(offsetX * difference, offsetY, player.transform.position.z), speed * Time.deltaTime);
        }
    }
}
