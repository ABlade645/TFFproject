using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxisFollow : MonoBehaviour
{
    public Transform objectToFollow;
    public bool self;

    public bool x;
    public bool y;

    Transform player;

    private void Start()
    {
        if (self)
        {
            objectToFollow = gameObject.GetComponent<Transform>();
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (x && y == false)
        {
            objectToFollow.position = new Vector2(player.position.x, objectToFollow.position.y);
        }

        if (y && x == false)
        {
            objectToFollow.position = new Vector2(objectToFollow.position.x, player.position.y);
        }

        if (x && y)
        {
            objectToFollow.position = new Vector2(player.position.x, player.position.y);
        }
    }
}
