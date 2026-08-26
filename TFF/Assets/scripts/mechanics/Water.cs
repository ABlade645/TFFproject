using UnityEngine;
using System.Collections.Generic;

public class Water : MonoBehaviour
{
    public float boyancy;
    public GameObject waterMask;
    List<Rigidbody2D> rbs = new List<Rigidbody2D>();

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.GetComponent<Rigidbody2D>() != null)
            rbs.Add(coll.GetComponent<Rigidbody2D>());

        if(coll.gameObject.CompareTag("Player"))
            waterMask.SetActive(true);       
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.GetComponent<Rigidbody2D>() != null)
            rbs.Remove(coll.GetComponent<Rigidbody2D>());

        if (coll.gameObject.CompareTag("Player"))
            waterMask.SetActive(false);
    }

    void FixedUpdate()
    {
        foreach (Rigidbody2D rb in rbs)
            if(rb.velocity.y > 0)
                rb.velocity += new Vector2(0, rb.velocity.y / boyancy) * Time.deltaTime;
            else
                rb.velocity -= new Vector2(0, rb.velocity.y / boyancy) * Time.deltaTime;
    }
}
