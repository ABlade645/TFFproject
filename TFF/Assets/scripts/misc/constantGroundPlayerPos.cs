using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constantGroundPlayerPos : MonoBehaviour
{
    RaycastHit2D hit;
    public GameObject posPoint;
    public LayerMask whatIsGround;
    public float yOffset;

    void Update()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, whatIsGround);

        posPoint.transform.position = new Vector2(hit.point.x, hit.point.y + yOffset);
    }
}
