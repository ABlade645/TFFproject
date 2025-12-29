using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource steps;
    public AudioSource swing;
    public AudioSource land;
    public playercontroller isgrounded;
    public GameObject player;


    public bool isWalking;
    public float CD;
    public float maxCD;

    public bool topDownMovement;
    float moveInputY;
    float moveInput;
    leftHand punchCD;

    public bool playSteps;

    void Update()
    {
        isgrounded = player.GetComponent<playercontroller>();

        player = GameObject.FindGameObjectWithTag("Player");

        moveInput = Input.GetAxis("Horizontal");


        if (topDownMovement == true)
            moveInputY = Input.GetAxis("Vertical");


        if (moveInput == 1 || moveInputY == 1 || moveInput == -1 || moveInputY == -1)
            isWalking = true;
        else
            isWalking = false;


        if (punchCD == null)
            punchCD = GameObject.Find("hand1").GetComponent<leftHand>();
        
        if(CD > 0)
            CD -= Time.deltaTime;

        if (playSteps == true)
            if (isWalking == true)
                if (CD <= 0)
                {
                    CD = maxCD;
                    steps.Play();
                }        
        

        if (player.GetComponent<playercontroller>().isGrounded == false && !topDownMovement)
            steps.Stop();
        

        if (Input.GetKeyUp(KeyCode.Q))     
            if (punchCD.timeBtwAttack <= 0)
                swing.Play();
    }
}
