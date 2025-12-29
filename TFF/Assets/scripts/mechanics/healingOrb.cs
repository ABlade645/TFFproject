using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healingOrb : MonoBehaviour
{
    bool isSpawned = true;
    bool moveTowards;
    public float delayTime;
    GameObject player;
    public int healAmount;
    int healValue;
    public int healOffset;
    public float spawnForce;
    public float speed;
    Rigidbody2D rb;
    public GameObject particle;

    void Update()
    {
        if (isSpawned == true)
        {
            int offsetX = Random.Range(-10, 10);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(offsetX, 1) * spawnForce);
            rb = gameObject.GetComponent<Rigidbody2D>();
            isSpawned = false;
            Invoke("MoveTowards", delayTime);
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (moveTowards == true)
        {
            rb.velocity = ((player.transform.position - transform.position).normalized * speed);
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            healValue = Random.Range(healAmount - healOffset, healAmount + healOffset);
            player.GetComponent<PlayerHealth>().health += healValue;
            Destroy(gameObject);
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }

    void MoveTowards()
    {
        moveTowards = true;
    }
}
