using UnityEngine;

public class CursorS : MonoBehaviour
{
    public GameObject cursor;

    void Update()
    {
        cursor.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Start()
    {
        Cursor.visible = false;
    }
}
