using UnityEngine;

public class InversedKinematicsLeg : MonoBehaviour
{
    [Header("General")]
    public Transform staticPos;
    public int length;

    [Header("Distance")]
    public float targetDist;

    [Header("Speed")]
    public float smoothSpeed;
    public float speed;

    [Header("By Object")]
    public GameObject objectToConnect;

    [Header("Leg function")]
    public GameObject targetObject;
    public Vector2 targetPoint;

    [Header("Angle")]
    public float offset;
    public bool drawGizmos;
    public bool useRestraints;
    public float allowedAngleU;
    public float allowedAngleL;

    float oDistance;
    bool hasSpawned = true;

    [HideInInspector]
    public Vector2[] segmentPoses;

    public GameObject[] line;

    void Start()
    {
        segmentPoses = new Vector2[line.Length];
    }

    void Update()
    {
        if (targetObject != null)
            targetPoint = (Vector2)targetObject.transform.position;

        if (hasSpawned)
        {
            for (int i = 0; i < line.Length; i++)
            {
                segmentPoses[i] = line[i].transform.position;
                if (i == line.Length - 1)                
                    hasSpawned = false;
                
            }
        }

        if (hasSpawned == false)
        {
            segmentPoses[segmentPoses.Length - 1] = Vector2.MoveTowards(segmentPoses[segmentPoses.Length - 1], targetPoint, speed * Time.deltaTime);
            line[0].transform.position = segmentPoses[0];
            line[segmentPoses.Length - 1].transform.position = segmentPoses[segmentPoses.Length - 1];

            Vector3 difference = ((Vector2)objectToConnect.transform.position - segmentPoses[0]).normalized;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            line[0].transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        }

        if (Vector2.Distance(objectToConnect.transform.position, segmentPoses[segmentPoses.Length - 1]) > targetDist * line.Length)
            segmentPoses[0] = segmentPoses[segmentPoses.Length - 1] + (((Vector2)objectToConnect.transform.position - segmentPoses[segmentPoses.Length - 1]).normalized * (targetDist * line.Length));
        

        if (Vector2.Distance(objectToConnect.transform.position, segmentPoses[segmentPoses.Length - 1]) < targetDist * line.Length)        
            segmentPoses[0] = segmentPoses[segmentPoses.Length - 1] + ((Vector2)objectToConnect.transform.position - segmentPoses[segmentPoses.Length - 1]);
        


        for (int i = 0; i < segmentPoses.Length - 2; i++)
        {
            Vector3 dif = segmentPoses[segmentPoses.Length - 1] - segmentPoses[segmentPoses.Length - 2];
            float rotZ = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
            line[segmentPoses.Length - 1].transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

            if (Vector2.Distance(segmentPoses[i + 1], segmentPoses[i + 2]) > targetDist)           
                //forward
                segmentPoses[i + 1] = Vector2.MoveTowards(segmentPoses[i + 1], segmentPoses[i + 2], speed * Time.deltaTime);
            
        }

        for(int i = 1; i < segmentPoses.Length - 1; i++)
        {
            Vector3 difference = segmentPoses[i - 1] - segmentPoses[i];
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            line[i].transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + 180);

            if (Vector2.Distance(segmentPoses[i], segmentPoses[i + 1]) < targetDist)
                segmentPoses[i] += (segmentPoses[i] - segmentPoses[i + 1]).normalized * (targetDist - Vector2.Distance(segmentPoses[i], segmentPoses[i + 1]));
            

            line[i].transform.position = segmentPoses[i];

            if (Vector2.Distance(segmentPoses[i], segmentPoses[i - 1]) > targetDist)
                segmentPoses[i] = Vector3.MoveTowards(segmentPoses[i], segmentPoses[i - 1], speed * Time.deltaTime);


            if (Vector2.Distance(segmentPoses[i], segmentPoses[i - 1]) < targetDist)
                segmentPoses[i] += (segmentPoses[i] - segmentPoses[i - 1]).normalized * (targetDist - Vector2.Distance(segmentPoses[i], segmentPoses[i - 1]));
            
            line[i].transform.position = segmentPoses[i];
        }
              
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if(drawGizmos)
        {
            Gizmos.DrawLine(segmentPoses[1], Quaternion.AngleAxis(allowedAngleU, -Vector3.forward) * segmentPoses[2]);
            Gizmos.DrawLine(segmentPoses[1], Quaternion.AngleAxis(allowedAngleL * -1, -Vector3.forward) * segmentPoses[2]);
        }      
    }
}
