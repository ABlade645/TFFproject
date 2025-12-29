using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoogAI : MonoBehaviour
{
    public GameObject explosion;
    public GameObject Object;
    bool canExplode;
    GameObject player;
    bool isLeft;

    public float maxDamageCD;
    float damageCD;

    public float Bhealth = 1f;

    void Start()
    {
        canExplode = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canExplode == true)
        {
            for (int i = 0; i < 1; i++)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
            }
            Invoke("Death", 0.1f);
            canExplode = false;
        }
    }

    void Update()
    {
        if (Bhealth < 1 && canExplode == true)
        {
            Invoke("Death", 0.1f);
            for (int i = 0; i < 1; i++)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
            }
            canExplode = false;
        }
        if (damageCD > 0)
        {
            damageCD -= Time.deltaTime;
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }


        if (GetComponentInParent<AIDestinationSetter>().target == null)
        {
            GetComponentInParent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (player.transform.position.x < transform.position.x && isLeft == true)
        {
            transform.localScale = -transform.localScale;
            isLeft = false;
        }

        if (player.transform.position.x > transform.position.x && isLeft == false)
        {
            transform.localScale = -transform.localScale;
            isLeft = true;
        }
    }

    public void Death()
    {
        Destroy(Object);
    }

    public void TakeDamage(float damage)
    {
        if (damageCD <= 0)
        {
            Bhealth -= damage;
        }
        damageCD = maxDamageCD;
    }
}
