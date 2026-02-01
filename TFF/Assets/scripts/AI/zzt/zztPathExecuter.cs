using System.Collections;
using UnityEngine;

public class zztPathExecuter : MonoBehaviour
{
    [Header("General")]
    public zztPathStorage[] paths;
    public float stopDistance;

    [Header("Gizmos")]
    public bool drawGizmos;
    public int gizmosIndex;

    [Header("Debug")]
    public int whatToExecute;
    public bool isExecuting;
    public int currentNode;

    InversedKinematicsLeg ik;
    float sqrDist;

    void Update()
    {
        if (isExecuting && currentNode < paths[whatToExecute].path.Length)
        {            
            if (sqrDist > Mathf.Pow((ik.segmentPoses[ik.segmentPoses.Length - 1] - paths[whatToExecute].path[currentNode]).magnitude, 2))
                ik.targetPoint = paths[whatToExecute].path[currentNode];
            else
                currentNode++;

            currentNode = 0;
        }
        else
        {
            if(isExecuting)
            {
                paths[whatToExecute].tentacle.SetActive(false);
                isExecuting = false;
            }       
        }     
    }

    public void Prepare(int index)
    {
        whatToExecute = index;

        paths[whatToExecute].tentacle.SetActive(true);
        ik = paths[whatToExecute].tentacle.GetComponentInParent<InversedKinematicsLeg>();

        for (int i = 0; i < ik.segmentPoses.Length; i++)        
            ik.segmentPoses[i] = paths[whatToExecute].startPos.position;

        sqrDist = stopDistance * stopDistance;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (drawGizmos)
            for (int i = 1; i < paths[gizmosIndex].path.Length; i++)
                Gizmos.DrawLine(paths[gizmosIndex].path[i - 1] + (Vector2)transform.position, paths[gizmosIndex].path[i] + (Vector2)transform.position);
    }
}
