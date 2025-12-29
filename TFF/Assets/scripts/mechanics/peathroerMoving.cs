using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peathroerMoving : MonoBehaviour
{
    public Vector2 difference;
    public float addValue;
    GameObject player;
    public GameObject peaThrower;
    public LayerMask mask;

    public float maxMoveCD;
    public float moveCD;

    public bool canMove;
    public float allowedDistance;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        moveCD -= Time.deltaTime;
        if (moveCD <= 0)
        {
            canMove = true;
        }

        if (canMove)
        {
            addValue = Random.Range(-5f, 5f);

            if (addValue < 0)
            {
                addValue += allowedDistance;
            }

            if (addValue > 0)
            {
                addValue -= allowedDistance;
            }

            moveCD = maxMoveCD;
            difference = new Vector2(player.transform.position.x + addValue, player.transform.position.y);

            //transform.position = difference;
            RaycastHit2D hit = Physics2D.Raycast(difference, new Vector2(difference.x, difference.y - 1), Mathf.Infinity);
            peaThrower.transform.position = hit.point;
            canMove = false;
        }

        
    }
}
