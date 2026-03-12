using UnityEngine;

public class playerFaceFollow : MonoBehaviour
{
    Transform player;
    Transform cursor;

    public float offset;
    public Vector3 offsetVec;

    float angle;
    float x, y;
    public float multX, multY;

    void Start()
    {
        cursor = GameObject.FindGameObjectWithTag("Cursor").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        transform.position = player.position + offsetVec + new Vector3(x, y) * offset;
        Vector2 vec = cursor.position - player.position;
        angle = Mathf.Atan2(vec.y, vec.x);
        x = Mathf.Cos(angle) * multX;
        y = Mathf.Sin(angle) * multY;

        if (player.position.x > cursor.position.x)
            GetComponent<SpriteRenderer>().flipX = true;
        

        if (player.position.x < cursor.position.x)
            GetComponent<SpriteRenderer>().flipX = false;
    }
}
