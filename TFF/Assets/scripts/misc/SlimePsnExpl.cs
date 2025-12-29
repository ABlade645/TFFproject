using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePsnExpl : MonoBehaviour
{
    [Header("Explosion")]
    public float maxDistance;
    float currentDistance;
    float expSpeed;
    public LayerMask whatIsPlayer;
    public int damage;
    public float force;

    [Header("Particle")]
    public GameObject particleF;
    public GameObject particleS;

    bool isSpawned = true;
    bool canExpand;
    bool canAttack = true;

    void Update()
    {
        if (isSpawned)
        {
            particleF.GetComponent<ParticleSystem>().Play();
            particleS.GetComponent<ParticleSystem>().Play();
            isSpawned = false;

            Invoke("Delete", 2.5f);
            canExpand = true;
        }

        if (canExpand)
        {
            if (currentDistance < maxDistance)
            {
                currentDistance += expSpeed * Time.deltaTime;
            }

            if (currentDistance >= maxDistance)
            {
                canExpand = false;
            }
        }

        Collider2D hit = Physics2D.OverlapCircle(transform.position, currentDistance, whatIsPlayer);

        if (canAttack && hit != null)
        {
            canAttack = false;
            hit.GetComponent<PlayerHealth>().health -= damage;
            hit.GetComponent<playercontroller>().hitStun = true;
            hit.GetComponent<Rigidbody2D>().velocity = (hit.transform.position - transform.position).normalized * force;
        }
    }

    void Delete()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
