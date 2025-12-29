using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restriction : MonoBehaviour
{
    GameObject player;
    GameObject hand;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hand = GameObject.FindGameObjectWithTag("Lhand");
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player.GetComponent<Dash>().canDash == true)
            {
                player.GetComponent<Dash>().canDash = false;
            }

            if (hand.GetComponent<leftHand>().canThrow == true)
            {
                hand.GetComponent<leftHand>().canThrow = false;
                hand.GetComponent<leftHand>().restrictedStone = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Dash>().canDash = true;
            hand.GetComponent<leftHand>().canThrow = true;
            hand.GetComponent<leftHand>().restrictedStone = false;
        }
    }    
}
