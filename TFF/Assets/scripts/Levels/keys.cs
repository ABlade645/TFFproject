using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keys : MonoBehaviour
{
    public GameObject keyA;
    public Transform point;
    public bool isHolded;
    bool canPick;

    public bool used;

    // Update is called once per frame
    void Update()
    {
        if (canPick && !used) 
        {
            if (!isHolded)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    isHolded = true;
                    canPick = false;
                }
            }
        }

        if (isHolded)
        {
            keyA.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            keyA.transform.position = point.position;
            keyA.GetComponent<BoxCollider2D>().isTrigger = true; 
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canPick = true;
        }
    }
}
