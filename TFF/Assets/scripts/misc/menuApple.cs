using UnityEngine;

public class menuApple : MonoBehaviour
{
    [Header("Rotation")]
    public float rotSpeed;
    float rotAngle;
    public bool canRotate;

    [Header("Offset movement")]
    public bool allowOffsetMovement;
    public float xOffset;
    public float yOffset;
    public float angle;
    public float moveSpeed;
    Vector2 startVector;

    void Start()
    {
        if (canRotate)       
            rotAngle = transform.rotation.z;
        

        if (allowOffsetMovement)       
            startVector = transform.position;
        
    }

    void Update()
    {
        if (canRotate)
        {
            rotAngle += rotSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, rotAngle);
        }

        if (allowOffsetMovement) 
        {
            angle += moveSpeed * Time.deltaTime;
            float x = Mathf.Cos(angle) * xOffset;
            float y = Mathf.Sin(angle) * yOffset;
            transform.position = startVector + new Vector2(x, y);
        }
    }
}
