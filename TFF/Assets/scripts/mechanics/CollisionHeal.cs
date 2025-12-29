using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHeal : MonoBehaviour
{
    public int heal;
    public string Target;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Target)
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            health.SetHealth(heal);

            Destroy(gameObject);
        }
    }
}
