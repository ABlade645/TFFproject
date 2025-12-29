using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEssenceAI : MonoBehaviour
{
    [Header("Detection Field")]
    public float distance;
    public LayerMask whatIsAlly;

    [Header("Position Calculation")]
    public float stoppingDist;
    public float returnSpeed;

    [Header("Gizmos")]
    public bool drawGizmos;

    bool isSpawned = true;
    Rigidbody2D rb;
    Vector3 startingPos;
    GameObject[] slimes;
    GameObject[] snekas;
    public GameObject[] allies;
    public string spawnAnim;
    //cache----
    EffectCache eCache;

    void Update()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, distance, whatIsAlly);

        if (isSpawned)
        {
            eCache = GameObject.FindGameObjectWithTag("EffectCache").GetComponent<EffectCache>();
            Instantiate(eCache.GetFromCache(spawnAnim), transform.position, Quaternion.identity);
            isSpawned = false;
            IsSpawned();
        }

        if (Vector2.Distance(startingPos, transform.position) > stoppingDist)
        {
            rb.velocity = (startingPos - transform.position).normalized * (returnSpeed + Vector3.Distance(startingPos, transform.position)) * Time.deltaTime;
        }

        if (hit != null)
        {
            foreach (Collider2D o in hit)
            {
                o.GetComponent<StatusEffects>().poison = true;
            }
        }         
    }

    void IsSpawned()
    {
        startingPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(startingPos, stoppingDist);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distance);
        }
    }
}
