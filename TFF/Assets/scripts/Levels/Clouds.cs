using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public Rigidbody2D rb;
    public playercontroller moveInput;
    public float speed;
    public GameObject player;
    public GameObject[] clouds;
    public Transform cloudPos;

    // Start is called before the first frame update
    void Start()
    {
        moveInput = player.GetComponent<playercontroller>();
        int n = Random.Range(0, 4);
        GameObject cl = Instantiate(clouds[n], cloudPos);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveInput.moveInput * -speed, rb.velocity.y);
    }
}
