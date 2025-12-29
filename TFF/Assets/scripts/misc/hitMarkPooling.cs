using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitMarkPooling : MonoBehaviour
{
    public GameObject[] hitmarks;
    playerAttack hitmark;

    void Start()
    {
        hitmark = GameObject.FindObjectOfType<playerAttack>();
    }

    public void SetHitmark()
    {
        int randInt = Random.Range(0, hitmarks.Length);
        hitmark.hitMark = hitmarks[randInt];
    }
}
