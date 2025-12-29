using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed;
    public float force;

    bool isSwimming;

    public GameObject Mask;

    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 5;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.gravityScale = 0.2f;
            isSwimming = true;
            Mask.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.gravityScale = 5;
            isSwimming = false;
            Mask.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSwimming == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                rb.velocity = new Vector2(rb.velocity.x, -10);
            }
        }
    }
}
