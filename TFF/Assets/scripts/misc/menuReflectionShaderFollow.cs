using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuReflectionShaderFollow : MonoBehaviour
{
    public GameObject camera;
    GameObject player;

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (camera.transform.position.x != player.transform.position.x)
        {
            camera.transform.position = new Vector3(player.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        }
    }
}
