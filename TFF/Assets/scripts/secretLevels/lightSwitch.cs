using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lightSwitch : MonoBehaviour
{
    public bool active;
    public bool canUse;
    bool canInterract;
    bool canTurnOff;
    public AudioSource sound;

    public GameObject interractTxt;

    float maxCD = 0.01f;
    float CD;

    public GameObject light;
    public Sprite on;
    public Sprite off;

    private void Start()
    {
        canUse = true;
        canTurnOff = true;
    }

    void On()
    {
        GetComponent<SpriteRenderer>().sprite = on;
        active = true;
        CD = maxCD;
        light.SetActive(true);
        sound.Play();
    }


    void Update()
    {
        if (canUse == true)
        {
            if (canInterract == true)
            {
                if (CD > 0)
                {
                    CD -= Time.deltaTime;
                }

                if (Input.GetKeyDown(KeyCode.Q) && CD <= 0 && active == false)
                {
                    Invoke("On", 0.01f);
                }

                if (Input.GetKeyDown(KeyCode.Q) && CD <= 0 && active == true)
                {
                    GetComponent<SpriteRenderer>().sprite = off;
                    active = false;
                    CD = maxCD;
                    light.SetActive(false);
                    sound.Play();
                }
            }
        }
        if(canUse == false && canTurnOff == true)
        {
            light.SetActive(false);
            canTurnOff = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canInterract = true;
            interractTxt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canInterract = false;
            interractTxt.SetActive(false);
        }
    }
}
