using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PeaThrower : MonoBehaviour
{
    GameObject player;
    public GameObject offsetObj;
    RaycastHit2D[] hits;
    RaycastHit2D hit;
    public int index;
    public LayerMask layer;
    public Collider2D coll;
    bool seen;
    public float waitTime;
    public float maxWaitTime;
    public float waitSpeed;
    public GameObject bullet;
    public Transform point;

    void Update()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        hits = Physics2D.RaycastAll(offsetObj.transform.position, new Vector2(player.transform.position.x, player.transform.position.y + 0.3f), Vector3.Distance(transform.position, player.transform.position));

        for (int i = 0; i < hits.Length ; i++)
        {
            hit = hits[i];
            coll = hit.collider;

            if (coll == player.GetComponent<CapsuleCollider2D>())
            {
                Anger();
            }
        }

        waitTime -= Time.deltaTime * waitSpeed;
    }


    void Anger()
    {
        if (waitTime <= 0)
        {
            Instantiate(bullet, point.position, Quaternion.identity);
            waitTime = maxWaitTime;
        }
    }   
    
    void Movement()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(offsetObj.transform.position, new Vector2(player.transform.position.x, player.transform.position.y + 0.3f));
    }
}
