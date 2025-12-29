using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveShroomTrigger : MonoBehaviour
{
    public float radius;
    public LayerMask whatIsPlayer;
    public GameObject explosion;

    private void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, whatIsPlayer);
        if (hit)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
