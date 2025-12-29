using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTerminal : MonoBehaviour
{
    public GameObject keyA;
    public GameObject keyB;
    bool canInterract;
    public bool terminalA;
    public bool terminalB;
    public Transform PosA;
    public Transform PosB;
    public GameObject DoorA;
    public GameObject DoorB;

    void Update()
    {

        if (canInterract == true)
        {
            if (keyA.GetComponent<keys>().isHolded == true)
            {
                terminalA = true;
                keyA.GetComponent<keys>().isHolded = false;
                keyA.transform.position = PosA.position;
                DoorA.SetActive(false);              
            }

            if (keyB.GetComponent<keys>().isHolded == true)
            {
                terminalB = true;
                keyB.GetComponent<keys>().isHolded = false;
                keyB.transform.position = PosB.position;
                DoorB.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canInterract = true;
            keyA.GetComponent<keys>().used = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canInterract = false;
        }
    }
}
