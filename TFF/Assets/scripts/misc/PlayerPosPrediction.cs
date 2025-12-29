using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPosPrediction : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector3 prediction;
    public float changeX;
    public float changeY;
    public float time;
    public playercontroller script;
    float speed;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //script = GetComponent<playercontroller>();
        speed = script.speed;
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        changeX = new Vector2(time, 0).magnitude * moveInput;
        changeY = new Vector2(0, transform.position.y).magnitude - (rb.gravityScale * time) * -1;
        prediction = (new Vector3(changeX, changeY, transform.position.z));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, prediction);
    }
}
