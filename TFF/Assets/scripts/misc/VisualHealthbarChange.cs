using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualHealthbarChange : MonoBehaviour
{
    PlayerHealth player;

    [Header("Health amount")]
    public int first;
    public int second;
    public int third;
    public int fourth;

    [Header("Images")]
    public Sprite First;
    public Sprite Second;
    public Sprite Third;
    public Sprite Fourth;
    public Sprite Fifth;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();   
    }

    void Update()
    {
        if (GetComponent<Image>().sprite != First && player.health >= first)
        {
            GetComponent<Image>().sprite = First;
        }

        if (GetComponent<Image>().sprite != Second && player.health >= second && player.health < first)
        {
            GetComponent<Image>().sprite = Second;
        }

        if (GetComponent<Image>().sprite != Third && player.health >= third && player.health < second)
        {
            GetComponent<Image>().sprite = Third;
        }

        if (GetComponent<Image>().sprite != Fourth && player.health <= fourth)
        {
            GetComponent<Image>().sprite = Fourth;
        }

        if (GetComponent<Image>().sprite != Fifth && player.health <= 0)
        {
            GetComponent<Image>().sprite = Fifth;
        }
    }
}
