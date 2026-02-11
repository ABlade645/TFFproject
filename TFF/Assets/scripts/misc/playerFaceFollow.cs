using UnityEngine;

public class playerFaceFollow : MonoBehaviour
{
    Transform player;
    Transform cursor;

    public float offset;
    public Vector3 offsetVec;

    void Start()
    {
        cursor = GameObject.FindGameObjectWithTag("Cursor").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        transform.position = player.position + offsetVec + (cursor.position - player.position).normalized * offset;
    }

    void Update()
    {
        if (player.position.x > cursor.position.x)
            GetComponent<SpriteRenderer>().flipX = true;
        

        if (player.position.x < cursor.position.x)
            GetComponent<SpriteRenderer>().flipX = false;
    }
}
