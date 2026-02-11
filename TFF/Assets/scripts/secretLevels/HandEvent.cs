using UnityEngine;

public class HandEvent : MonoBehaviour
{
    public bool canTrigger;
    public float offset;
    public GameObject hand;

    Transform player;
    Transform cursor;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cursor = GameObject.FindGameObjectWithTag("Cursor").transform;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && canTrigger)
        {
            canTrigger = false;
            hand.SetActive(true);
            hand.transform.position = (player.position - cursor.position).normalized * offset + player.position;
        }                
    }
}
