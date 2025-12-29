using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuTVRotation : MonoBehaviour
{
    public float rotationSpeed;
    float rotation = 0f;
    Transform newRotation;

    void Update()
    {
        transform.rotation = new Quaternion(0, 0, rotationSpeed * Time.deltaTime, 0);

        //rotation += rotationSpeed * Time.deltaTime;
    }
}
