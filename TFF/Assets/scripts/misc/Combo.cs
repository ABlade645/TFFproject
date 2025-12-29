using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    public GameObject text;
    Animator anim;
    public float damageCount;
    public bool isActive;

    public float time;
    public float maxTime;

    void Start()
    {
        if (text == null)
        {
            text = GameObject.Find("number");
        }

        if (text != null)
        {
            if (text.activeSelf) 
            {
                text.SetActive(false);
                anim = text.GetComponent<Animator>();
            }
        }
    }

    void Update()
    {
        if (isActive)
        {
            text.SetActive(true);
            time -= Time.deltaTime;

            text.GetComponent<Text>().text = damageCount.ToString();
        }

        if (time <= 0 && text.activeSelf)
        {
            isActive = false;
            text.SetActive(false);
            damageCount = 0;
        }
    }

    public void TextOffset()
    {
        anim.Play("Damage counter");
    }
}
