using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Collider2D[] inExplosionRadius = null;
    [SerializeField] public float ExplosionForceMulti = 1000;
    [SerializeField] public float ExplosionRadius = 1;
    bool exploded = false;
    public Rigidbody2D[] o_rigidbody;
    public float speed;
    bool exploding;
    public LayerMask whatIsEntity;
    public int explDamage;
    bool canDamage;

    public GameObject[] particle;
    GameObject particleNew;
    GameObject boog;

    private void Update()
    {
        particle = GameObject.FindGameObjectsWithTag("Explosion");

        if (exploded == false)
        {
            Invoke("Explode", 0.1f);
            canDamage = true;           
        }

        ExplosionRadius += Time.deltaTime * speed;
        if (ExplosionRadius >= 10)
        {
            ExplosionRadius = 10;
            Invoke("Death", 0.1f);
        }


        if (exploding == true)
        {
            Collider2D[] entitiesToDamage = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius, whatIsEntity);
            foreach (Collider2D o in entitiesToDamage)
            {
                Collider2D entity = o.GetComponent<Collider2D>();
                for (int i = 0; i < entitiesToDamage.Length; i++)
                {
                    if (canDamage == true)
                    {
                        if (entitiesToDamage[i].tag == "Player")
                        {
                            entitiesToDamage[i].GetComponent<PlayerHealth>().ExplosionDamage(explDamage);
                            entitiesToDamage[i].GetComponent<playercontroller>().hitStun = true;

                            Vector2 distanceVector = entitiesToDamage[i].transform.position - transform.position;
                            entitiesToDamage[i].GetComponent<Rigidbody2D>().AddForce(distanceVector.normalized * ExplosionForceMulti * 10);
                        }

                        
                        canDamage = false;
                    }
                    exploding = false;
                }
            }
        }
    }
 
    public void Explode()
    {
        for (int i = 0; i < particle.Length; i++)
        {
            particleNew = particle[i];
            particleNew.GetComponent<ParticleSystem>().Play();
        }

        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius, whatIsEntity);
        if (inExplosionRadius != null)
        {
            o_rigidbody = new Rigidbody2D[inExplosionRadius.Length];
        }
        exploding = true;
        
        foreach (Collider2D o in inExplosionRadius)
        {
            for (int i = 0; i < inExplosionRadius.Length - 1; i++)
            {
                o_rigidbody[i] = inExplosionRadius[i].GetComponent<Rigidbody2D>();
                if (o_rigidbody != null)
                {
                    Vector2 distanceVector = o.transform.position - transform.position;
                    if (distanceVector.magnitude > 0)
                    {

                        //float explosionForce = ExplosionForceMulti;
                        o_rigidbody[i].AddForce(distanceVector.normalized * ExplosionForceMulti);

                        if (i == inExplosionRadius.Length - 1)
                        {
                            exploded = false;
                        }                       
                    }                
                }
            }           
        }        
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
