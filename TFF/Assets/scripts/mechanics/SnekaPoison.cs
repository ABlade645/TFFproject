using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekaPoison : MonoBehaviour
{
    [Header("Misc")]   
    public ParticleSystem trail;
    public GameObject explosion;

    [Header("Damage properties")]
    public int snDamage;
    public float attackDistance;
    public LayerMask whatIsPlayer;

    [Header("Calculation properties")]
    public float knForce;
    public float force;
    public LayerMask obstacle;
    public float maxTime = 0.01f;
    float time;

    GameObject player;
    bool canDrip = true;
    bool isAttacking = true;
    Vector2 buffer;

    private void Update()
    {
        if (canDrip == true)
        {
            canDrip = false;
            trail.Play();
            Invoke("Apply", 0.02f);
            time = maxTime;
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (isAttacking == true)
        {
            
        }

        if (time > 0)
        {
            time -= Time.deltaTime;
        }
    }

    void Apply()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2((player.transform.position.x - transform.position.x), (player.transform.position.y - transform.position.y)).normalized * force;
        buffer = new Vector2((player.transform.position.x - transform.position.x), (player.transform.position.y - transform.position.y)).normalized;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            player.GetComponent<Rigidbody2D>().velocity += buffer * knForce;
            buffer = Vector2.zero;
            player.GetComponent<playercontroller>().hitStun = true;

            Collider2D[] playersToDamage = Physics2D.OverlapCircleAll(transform.position, attackDistance, whatIsPlayer);
            for (int i = 0; i < playersToDamage.Length; i++)
            {
                playersToDamage[i].GetComponent<PlayerHealth>().takeDamage(snDamage);
                isAttacking = false;
            }
        }
        
        if (collision.gameObject.tag == "Slime" && time <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            buffer = Vector2.zero;

            Collider2D[] playersToDamage = Physics2D.OverlapCircleAll(transform.position, attackDistance, whatIsPlayer);
            for (int i = 0; i < playersToDamage.Length; i++)
            {
                playersToDamage[1].GetComponentInParent<StatusEffects>().poison = true;
                isAttacking = false;
            }
        }

        if (collision.gameObject.tag == "Ground")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
