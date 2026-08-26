using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public Vector2 start;
    public Vector2 end;
    public Vector2 offset;
    public float speed;
    [HideInInspector]
    public bool selected;

    public RectTransform text;

    void Start()
    {
        start = transform.position;
        end = start + offset;
    }

    public void Up()
    {
        if(!selected)
            selected = true;

        if((Vector2)transform.position != end)
            transform.position = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime);
    }

    void Update()
    {
        if (text.position != transform.position)
            text.position = transform.position;


        if (!selected && (Vector2)transform.position != start)
            transform.position = Vector3.MoveTowards(transform.position, start, speed * Time.deltaTime);
    }
}
