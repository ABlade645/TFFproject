using UnityEngine;

public class playerHandFollow : MonoBehaviour
{
    Transform player;
    Transform cursor;
    public float speedMultiplier;
    public float impulseLength;

    bool follows;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cursor = GameObject.FindGameObjectWithTag("Cursor").transform;
        follows = true;
    }

    void FixedUpdate()
    {
        if(follows)
        {
            Vector3 vec = (player.position - transform.position);
            transform.position += vec.normalized * (vec.magnitude * speedMultiplier);
        }          
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
            transform.position -= (player.position - cursor.position).normalized * impulseLength;
    }
}
