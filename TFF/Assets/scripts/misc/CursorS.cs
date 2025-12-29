using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorS : MonoBehaviour
{
    public GameObject cursor;
    Vector3 mouse;

    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouse.z = 0;

        cursor.transform.position = mouse;
    }

    void Start()
    {
        Cursor.visible = false;
    }
}
