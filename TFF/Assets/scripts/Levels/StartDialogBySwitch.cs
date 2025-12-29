using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogBySwitch : MonoBehaviour
{
    bool canStart;
    bool can;
    public DialogTrigger script;

    private void Start()
    {
        can = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canStart == true && can == true)
        {
            script.TriggerDialog();
            can = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canStart = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canStart = false;
        }
    }
}
