using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public GameObject cursor;

    bool isLeft;
    bool isRight;
    bool isJumping;
    bool secondaryJump;

    playercontroller standart;
    FourWayMovement secondary;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        secondary = GetComponent <FourWayMovement>();
        standart = GetComponent<playercontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        //direction by cursor
        if (cursor.transform.position.x > player.transform.position.x)
        {
            isLeft = false;
            isRight = true;
        }

        if (cursor.transform.position.x < player.transform.position.x)
        {
            isLeft = true;
            isRight = false;
        }

        if (GetComponent<playercontroller>().enabled == true)
        {
            if (GetComponent<playercontroller>().timeBeforeJump > 0)
            {
                if (GetComponent<playercontroller>().isGrounded == true)
                {
                    isJumping = true;
                    anim.CrossFade("Player_Jump", 0, 0);
                }
            }

            if (isJumping == false)
            {
                if (isLeft == true)
                {
                    if (standart.moveInput != 1 && standart.moveInput != -1)
                    {
                        anim.CrossFade("Player_Left", 0, 0);
                    }

                    if (standart.moveInput != 0)
                    {
                        anim.CrossFade("Player_walkLeft", 0, 0);
                    }
                }

                if (isRight == true)
                {
                    if (standart.moveInput != 1 && standart.moveInput != -1)
                    {
                        anim.CrossFade("Player_Right", 0, 0);
                    }

                    if (standart.moveInput != 0)
                    {
                        anim.CrossFade("Player_Walk", 0, 0);
                    }
                }
            }            
        }

        //topdown movement
        if (GetComponent<playercontroller>().enabled == false)
        {
            if (isLeft == true)
            {
                if (secondary.moveInputX != 1 && secondary.moveInputX != -1)
                {
                    if (secondary.moveInputY == 0)
                    {
                        anim.CrossFade("Player_Left", 0, 0);
                    }
                }

                if (secondary.moveInputX != 0)
                {
                    anim.CrossFade("Player_walkLeft", 0, 0);
                }

                if (secondary.moveInputY != 1 && secondary.moveInputY != -1)
                {
                    if (secondary.moveInputX == 0)
                    {
                        anim.CrossFade("Player_Left", 0, 0);
                    }
                }

                if (secondary.moveInputY != 0)
                {
                    anim.CrossFade("Player_walkLeft", 0, 0);
                }
            }

            if (isRight == true)
            {
                if (secondary.moveInputX != 1 && secondary.moveInputX != -1)
                {
                    if (secondary.moveInputY == 0)
                    {
                        anim.CrossFade("Player_Right", 0, 0);
                    }
                }

                if (secondary.moveInputX != 0)
                {
                    anim.CrossFade("Player_Walk", 0, 0);
                }

                if (secondary.moveInputY != 1 && secondary.moveInputY != -1)
                {
                    if (secondary.moveInputX == 0)
                    {
                        anim.CrossFade("Player_Right", 0, 0);
                    }
                }

                if (secondary.moveInputY != 0)
                {
                    anim.CrossFade("Player_Walk", 0, 0);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stone" && GetComponentInChildren<playercontroller>().isGrounded == false)
        {
            if (true)
            {
                
            }
        }
    }

    public void EndJump()
    {
        isJumping = false;
        //secondaryJump = false;
    }

    public void SecondaryJump()
    {
        secondaryJump = true;
        if (GetComponent<playercontroller>().moveInput > 0 && GetComponent<playercontroller>().moveInput == 0)
        {
            anim.CrossFade("Player_JumpRight", 0, 0);
        }

        if (GetComponent<playercontroller>().moveInput < 0)
        {
            anim.CrossFade("Player_JumpLeft", 0, 0);
        }
    }
}
