using UnityEngine.UI;
using UnityEngine;

public class ScrollBarUI : MonoBehaviour
{
    public bool flipDirection;

    public Scrollbar scrollBar;
    public float scrollAmount;
    Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (!flipDirection) 
        {
            if (transform.position.y != startPos.y - scrollAmount * scrollBar.value)
            {
                transform.position = new Vector2(transform.position.x, startPos.y - scrollAmount * scrollBar.value);
            }
        }
        else
        {
            if (transform.position.y != startPos.y + scrollAmount * scrollBar.value)
            {
                transform.position = new Vector2(transform.position.x, startPos.y + scrollAmount * scrollBar.value);
            }         
        }
    }
}
