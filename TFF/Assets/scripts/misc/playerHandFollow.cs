using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHandFollow : MonoBehaviour
{
    GameObject player;    
    public float baseSpeed;
    public float speedMultiplier;
    float speed;

    GameObject cursor;
    public float impulse;
    public bool canImpulse;
    public float waitTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cursor = GameObject.FindGameObjectWithTag("Cursor");
    }

    void Update()
    {
        if (transform.position != player.transform.position)
        {
            speed = baseSpeed * (speedMultiplier * Vector2.Distance(transform.position, player.transform.position));
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && canImpulse == true)
        {
            Invoke("Impulse", waitTime);
            canImpulse = false;
        }
    }

    void Impulse()
    {
        transform.position = Vector2.MoveTowards(transform.position, cursor.transform.position, impulse * Time.deltaTime);
        canImpulse = true;
    }
}
