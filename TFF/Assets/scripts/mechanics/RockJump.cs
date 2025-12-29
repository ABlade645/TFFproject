using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockJump : MonoBehaviour
{
    public Rigidbody2D rb;
    public float JumpForce;
    public bool canJumpOR;
    public Transform groundCheck;
    bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;

    float force;
    public GameObject hand;


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded == true)
        {
            canJumpOR = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stone" && canJumpOR == true)
        {
            if (isGrounded == false)
            {
                rb.velocity = Vector2.up * JumpForce;
                canJumpOR = false;
                //GetComponentInParent<PlayerAnimation>().SecondaryJump();
            }
        }
    }
}
