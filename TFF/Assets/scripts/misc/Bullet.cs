using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D playerRb;
    public float time;
    public float distance;
    public float force;
    int direction;
    bool canShoot = true;
    public float lifetime;
    public float speed;

    void Update()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();

        if (transform.position.x <= player.transform.position.x)
        {
            direction = 1;
        }else if (transform.position.x >= player.transform.position.x)
        {
            direction = -1;
        }

        if (GameObject.FindGameObjectWithTag("bullet") && canShoot)
        {
            GetComponent<Rigidbody2D>().velocity = player.transform.position * force * direction;
            canShoot = false;
        }

        lifetime -= speed;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
