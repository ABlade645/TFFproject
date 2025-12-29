using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamCollision : MonoBehaviour
{
    public GameObject entity;
    Rigidbody2D rb;
    public float force;

    private void Start()
    {
        rb = entity.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slam")
        {
            rb.AddForce(Vector2.up * force);
        }
    }
}
