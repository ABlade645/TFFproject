using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothCamera : MonoBehaviour
{
    public float dumping = 1.5f;
    public Vector2 offset = new Vector2(0f,0f);
    public bool isLeft;
    private Transform player;
    public Rigidbody2D rb;
    private int LastX;
    public Vector3 CamPos;
    public GameObject Camera;
    public float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(isLeft);
    }

    public void FindPlayer(bool playerIsLeft)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        LastX = Mathf.RoundToInt(player.position.x);
        if (playerIsLeft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        CamPos = Camera.GetComponent<Transform>().position;

        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > LastX) isLeft = false; else if (currentX < LastX) isLeft = true;
            LastX = Mathf.RoundToInt(player.position.x);

            Vector3 target;

            if (isLeft)
            {
                target = new Vector3(player.position.x - offset.x, player.position.y - offset.y + offsetY, transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y + offsetY, transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}
