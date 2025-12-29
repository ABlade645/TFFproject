using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZTBullet : MonoBehaviour
{
    bool isSpawned = true;
    public float impulse;
    public float knockback;
    GameObject player;

    public int ADamage;
    public float attackDistance;

    public LayerMask ignoreLayer;

    bool canBeDestroyed;

    public Sprite[] sprite;
    float spriteChangeInterval;
    public float maxSpriteChangeInterval;

    public float lifetime;

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }

        if (isSpawned == true)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * impulse;
            isSpawned = false;
        }

        if (spriteChangeInterval <= 0)
        {
            int spriteNum = Random.Range(0, sprite.Length);
            GetComponent<SpriteRenderer>().sprite = sprite[spriteNum];
            spriteChangeInterval = maxSpriteChangeInterval;
        }

        if (spriteChangeInterval > 0)
        {
            spriteChangeInterval -= Time.deltaTime;
        }


        Collider2D[] playersToDamage = Physics2D.OverlapCircleAll(transform.position, attackDistance, ignoreLayer);
        for (int i = 0; i < playersToDamage.Length; i++)
        {
            if (playersToDamage[0] == player.GetComponent<Collider2D>())
            {
                playersToDamage[0].GetComponent<PlayerHealth>().ZZTPrjctlDamage(ADamage);
                playersToDamage[0].GetComponent<playercontroller>().hitStun = true;
                playersToDamage[0].GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * knockback;
                Destroy(gameObject);
            }

            if (playersToDamage[0] != player.GetComponent<Collider2D>())
            {
                Destroy(gameObject);
            }
        }

        if (canBeDestroyed)
        {
            
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
