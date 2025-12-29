using System.Collections;
using UnityEngine;

public class minecart : MonoBehaviour
{
    Rigidbody2D rb;

    public float allowedRot;

    bool canCheck = true;

    void Update()
    {
        if (canCheck)
        {
            canCheck = false;
            CheckUp();
        }

        if (transform.rotation.z > allowedRot)
        {
            transform.rotation = new Quaternion(0, 0, allowedRot, 0);
        }

        if (transform.rotation.z < -allowedRot)
        {
            transform.rotation = new Quaternion(0, 0, -allowedRot, 0);
        }
    }

    void CheckUp()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
