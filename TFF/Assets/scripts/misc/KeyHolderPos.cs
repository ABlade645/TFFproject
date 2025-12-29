
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolderPos : MonoBehaviour
{
    playercontroller moveInput;
    Rigidbody2D playerRb;
    GameObject player;
    public float speed;
    public float maxThreshold;
    public bool drawGizmos;

    public float yThreshold;


    void Start()
    {
        moveInput = GetComponentInParent<playercontroller>();
        playerRb = GetComponentInParent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        //yThreshold = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x != moveInput.moveInput)
        {

            if (moveInput.moveInput != 0)
            {
                float targetX = Mathf.Clamp(player.transform.position.x + moveInput.moveInput * maxThreshold,
                                         player.transform.position.x - maxThreshold,
                                         player.transform.position.x + maxThreshold);

                Vector2 targetPos = new Vector2(targetX, player.transform.position.y + yThreshold);

                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }

            if (transform.position.x > player.transform.position.x + maxThreshold)
            {
                transform.position = new Vector2(player.transform.position.x + maxThreshold, player.transform.position.y + yThreshold);
            }

            if (transform.position.x < player.transform.position.x - maxThreshold)
            {
                transform.position = new Vector2(player.transform.position.x - maxThreshold, player.transform.position.y + yThreshold);
            }
        }
    }


    void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine((Vector2)player.transform.position + new Vector2(maxThreshold, transform.position.y), (Vector2)player.transform.position + new Vector2(player.transform.position.x - maxThreshold, transform.position.y));
        }
    }
}

