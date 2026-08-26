using UnityEngine;

public class flashLightEvents : MonoBehaviour
{
    RaycastHit2D hit;
    GameObject cursor;
    public GameObject hand;

    public float distance;
    public LayerMask layer;

    Vector2 vec;
    keyDoor door;

    void Setup()
    {
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        //door = GameObject.Find("doorBlue").GetComponent<keyDoor>();
    }

    void Update()
    {
        if (gameObject.activeSelf && cursor == false)
            Setup();

        vec = cursor.transform.position - transform.position;

        hit = Physics2D.Raycast(transform.position, vec.normalized, distance, layer);

        if(hit)
        {
            //door.hasKey = true;
            //door.InfoC = Color.white;
            //door.InfoS = "A card is needed";
            //door.Info.SetActive(false);
            hand.SetActive(false);
        }
          
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + vec.normalized * distance);
    }
}
