using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    GameObject player;
    public GameObject cursor;

    bool isLeft;
    bool isRight;
    bool isJumping;
    //bool secondaryJump;

    float moveInput;

    FourWayMovement secondary;
    playercontroller primary;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        secondary = GetComponent<FourWayMovement>();
        primary = GetComponent<playercontroller>();
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

        if (secondary == null)
        {
            moveInput = Input.GetAxis("Horizontal");

            if (primary.timeBeforeJump > 0)           
                if (primary.isGrounded == true)
                {
                    isJumping = true;
                    anim.CrossFade("Player_Jump", 0, 0);
                }
                       

            if (isJumping == false)
            {
                if (isLeft == true)
                {
                    if (moveInput != 1 && moveInput != -1)                   
                        anim.CrossFade("Player_Left", 0, 0);
                    
                    if (moveInput != 0)                
                        anim.CrossFade("Player_walkLeft", 0, 0);                    
                }

                if (isRight == true)
                {
                    if (moveInput != 1 && moveInput != -1)                   
                        anim.CrossFade("Player_Right", 0, 0);                   

                    if (moveInput != 0)                   
                        anim.CrossFade("Player_Walk", 0, 0);                   
                }
            }            
        }

        //topdown movement
        else
        {
            if (isLeft == true)
            {
                if (secondary.moveInputX != 1 && secondary.moveInputX != -1)                
                    if (secondary.moveInputY == 0)                   
                        anim.CrossFade("Player_Left", 0, 0);                                  

                if (secondary.moveInputX != 0)              
                    anim.CrossFade("Player_walkLeft", 0, 0);             

                if (secondary.moveInputY != 1 && secondary.moveInputY != -1)               
                    if (secondary.moveInputX == 0)                   
                        anim.CrossFade("Player_Left", 0, 0);
                                   
                if (secondary.moveInputY != 0)                
                    anim.CrossFade("Player_walkLeft", 0, 0);              
            }

            if (isRight == true)
            {
                if (secondary.moveInputX != 1 && secondary.moveInputX != -1)                
                    if (secondary.moveInputY == 0)                   
                        anim.CrossFade("Player_Right", 0, 0);                                  

                if (secondary.moveInputX != 0)              
                    anim.CrossFade("Player_Walk", 0, 0);               

                if (secondary.moveInputY != 1 && secondary.moveInputY != -1)                
                    if (secondary.moveInputX == 0)                    
                        anim.CrossFade("Player_Right", 0, 0);                   
                
                if (secondary.moveInputY != 0)               
                    anim.CrossFade("Player_Walk", 0, 0);                
            }
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Stone" && GetComponentInChildren<playercontroller>().isGrounded == false)
    //    {
    //        if (true)
    //        {
                
    //        }
    //    }
    //}

    public void EndJump()
    {
        isJumping = false;
        //secondaryJump = false;
    }

    public void SecondaryJump()
    {
        //secondaryJump = true;
        if (moveInput > 0 && moveInput == 0)       
            anim.CrossFade("Player_JumpRight", 0, 0);
        
        if (moveInput < 0)        
            anim.CrossFade("Player_JumpLeft", 0, 0);       
    }
}
