using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smolStone : MonoBehaviour
{
    public GameObject stone;
    public float force;
    public float pForce;
    bool isSpawned = true;

    public GameObject Direction;
    public GameObject dirPos;

    public Transform attackPos;
    public float attackDistance;
    public LayerMask whatIsEnemy;
    public int damage;

    public float startTimeBtwAttack;
    float timeBtwAttack;
    public float lifeTime;

    float stonePos;
    Transform playerPos;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Direction.transform.position = dirPos.transform.position;

        if (GameObject.FindGameObjectWithTag("smolStone") && isSpawned == true)
        {
            stone.GetComponent<Rigidbody2D>().AddForce(Direction.transform.localPosition * force);
            isSpawned = false;
        }

        if (timeBtwAttack <= 0)
        {
            timeBtwAttack = startTimeBtwAttack;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackDistance, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<SlimeAI>().TakeDamageRanged(damage);
            }
        }

        timeBtwAttack -= Time.deltaTime;

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            lifeTime = 0.1f;
        }

        if (collision.gameObject.tag == "Slime")
        {
            lifeTime = 0.05f;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackDistance);
    }
}
