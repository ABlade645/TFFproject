using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorS : MonoBehaviour
{
    public GameObject cursor;

    void FixedUpdate()
    {
        cursor.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Start()
    {
        Cursor.visible = false;
    }
}
