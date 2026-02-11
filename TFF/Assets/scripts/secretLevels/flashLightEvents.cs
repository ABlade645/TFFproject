using UnityEngine;

public class flashLightEvents : MonoBehaviour
{
    RaycastHit2D hit;
    GameObject cursor;
    GameObject hand;

    public float distance;
    public LayerMask layer;

    Vector2 vec;

    void Update()
    {
        if (cursor == null)
        {
            cursor = GameObject.FindGameObjectWithTag("Cursor");
            hand = GameObject.Find("DaHand");
        }

        vec = cursor.transform.position - transform.position;

        hit = Physics2D.Raycast(transform.position, vec.normalized, distance, layer);

        if(hit)
            hand.SetActive(false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + vec.normalized * distance);
    }
}
