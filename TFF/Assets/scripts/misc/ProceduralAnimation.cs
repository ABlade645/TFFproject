using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralAnimation : MonoBehaviour
{
    [Header("Forward Kinematics")]
    public Transform direction;
    public float startDistance;
    public float speed;
    public float offset;

    [Header("Rigidbody")]
    Rigidbody2D rb;

    [Header("Inversed Kinematics")]
    public bool enable;
    public Transform backDirection;

    [Header("Outline")]
    public bool outlineEnabled;

    [Header("Walk")]
    public bool canWalk;
    public GameObject joint;
    public float maxEnabledTime;
    float enabledTime;
    public float maxAngle;

    float angle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 difference = direction.position - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

        if (Vector2.Distance(transform.position, direction.position) > startDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, direction.position, speed);
        }

        if (enable)
        {
            if (Vector2.Distance(transform.position, backDirection.position) > startDistance + speed)
            {
                transform.position = Vector2.MoveTowards(transform.position, backDirection.position, speed);
            }
        }

        if (canWalk)
        {
            if (Vector2.Angle(transform.position, joint.transform.position) > maxAngle && enabledTime <= 0)
            {
                joint.GetComponent<ProceduralAnimation>().enabled = true;
                enabledTime = maxEnabledTime;
            }

            if (enabledTime > 0)
            {
                enabledTime -= Time.deltaTime;
            }
            
            if(enabledTime <= 0)
            {
                joint.GetComponent<ProceduralAnimation>().enabled = false;
            }
        }
    }

    

    private void OnDrawGizmos()
    {
        if (outlineEnabled)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, startDistance);
        }
    }
}
