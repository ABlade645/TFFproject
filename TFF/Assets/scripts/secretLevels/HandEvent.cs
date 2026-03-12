using UnityEngine;

public class HandEvent : MonoBehaviour
{
    public bool canTrigger;
    public float offset;
    public GameObject hand;
    keyDoor door;

    Transform player;
    Transform cursor;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cursor = GameObject.FindGameObjectWithTag("Cursor").transform;
        door = GameObject.Find("doorBlue").GetComponent<keyDoor>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && canTrigger)
        {
            door.hasKey = false;
            door.InfoC = Color.red;
            door.InfoS = "The door is closed";
            door.Info.SetActive(false);
            gameObject.SetActive(false);
            hand.SetActive(true);
            hand.transform.position = (player.position - cursor.position).normalized * offset + player.position;
        }                
    }
}
