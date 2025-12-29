using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCutsceneF : MonoBehaviour
{
    public GameObject mimic;
    GameObject player;
    BoxCollider2D coll;
    public float maxDistance;
    float distance;
    public float delayTime;
    Rigidbody2D mimicRb;
    public float speed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        coll = GetComponent<BoxCollider2D>();

        mimic.SetActive(false);       
    }

    private void Update()
    {
        if (mimic.activeSelf)
        {
            distance = Vector2.Distance(player.transform.position, mimic.transform.position);
        }

        if (distance <= maxDistance)
        {
            mimic.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mimic.SetActive(true);
            coll.enabled = false;
            mimicRb = mimic.GetComponent<Rigidbody2D>();
        }
    }

    void Delay()
    {
        mimic.SetActive(false);
    }
}
