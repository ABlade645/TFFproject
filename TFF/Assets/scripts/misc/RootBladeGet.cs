using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBladeGet : MonoBehaviour
{
    public playerAttack script;
    public GameObject Info;

    bool canGet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Info.SetActive(true);
            canGet = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Info.SetActive(false);
            canGet = false;
        }
    }

    private void Update()
    {
        if (canGet)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                script.GetRootBlade();
            }
        }
    }
}
