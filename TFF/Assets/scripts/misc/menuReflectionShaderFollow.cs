using UnityEngine;

public class menuReflectionShaderFollow : MonoBehaviour
{
    public GameObject camera;
    public Transform target;

    public bool holdY;
    public bool holdX;
    float X, Y;

    void Update()
    {
        if (holdY)
        {
            if (Y != transform.position.y)
                Y = transform.position.y;
        }            
        else
            Y = target.position.y;

        if (holdX)
        {
            if(X != transform.position.x)
                X = transform.position.x;
        }
        else
            X = target.position.x;

        if (camera.transform.position.x != target.position.x)       
            camera.transform.position = new Vector3(X, Y);       
    }
}
