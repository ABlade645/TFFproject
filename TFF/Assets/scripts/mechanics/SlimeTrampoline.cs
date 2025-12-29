using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTrampoline : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject slam;

    public float force;
    bool isColliding;

    void Update()
    {
        if (slam.GetComponent<Slam>().canSlam == true)
        {
            if (isColliding == true)
            {
                rb.AddForce(Vector2.up * force);

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            isColliding = false;
        }
    }
}
