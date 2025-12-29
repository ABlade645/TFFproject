using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeJumpDir : MonoBehaviour
{
    public float offset;
    public GameObject player;

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 difference = player.transform.position - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
    }
}
