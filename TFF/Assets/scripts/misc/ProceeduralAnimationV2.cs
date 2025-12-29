using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProceeduralAnimationV2 : MonoBehaviour
{
    [Header("General")]
    public bool autoTarget;
    public GameObject ghostHolder;
    public GameObject ghost;
    public Transform direction;
    public Transform targetDir;
    public int length;
    public float allowedSegmentDisance;

    [Header("Distance")]
    public float targetDist;
    public float minFollowDistance;

    [Header("Speed")]
    public float smoothSpeed;
    public float speed;

    [Header("States")]
    public bool head;
    public bool body;

    [Header("Crawl properties")]
    public float gravityBuffer;

    Vector3[] segmentPoses;
    Vector3[] segmentV;

    float offset;
    public GameObject[] segments;
    int random;
    bool isSpawned;


    void Start()
    {

        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
    }

    void Update()
    {
        if (isSpawned == false)
        {
            for (int i = 1; i < segmentPoses.Length; i++)
            {
                segmentPoses[i] = segments[i].transform.position;
                if (i == segmentPoses.Length - 1)
                {
                    isSpawned = true;
                }
            }
        }

        if (head)
        {
            if (autoTarget == true)
            {
                if (direction == null)
                {
                    direction = GameObject.FindGameObjectWithTag("Player").transform;
                }

                if (GetComponentInParent<AIDestinationSetter>().target == null)
                {
                    ghostHolder = GameObject.FindGameObjectWithTag("ghost4");
                    random = Random.Range(1, 4);
                    ghost = ghostHolder.GetComponent<ghostArrayBuffer>().ghosts[random];
                    GetComponentInParent<AIDestinationSetter>().target = ghost.transform;
                }
            }

            Vector3 difference = direction.position - transform.position;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        }

        if (isSpawned == true)
        {

            

            if (body)
            {
                segmentPoses[0] = targetDir.position;

                for (int i = 1; i < segmentPoses.Length; i++)
                {
                    Vector3 difference = segmentPoses[i - 1] - segmentPoses[i];
                    float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    segments[i].transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

                    if (Vector2.Distance(segmentPoses[i], segmentPoses[i - 1]) > allowedSegmentDisance)
                    {
                        //segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1], ref segmentV[i], smoothSpeed);
                        segmentPoses[i] = Vector3.MoveTowards(segmentPoses[i], segmentPoses[i - 1], smoothSpeed * Time.deltaTime);
                    }

                    if (Vector2.Distance(segmentPoses[i], segmentPoses[i - 1]) < targetDist)
                    {
                        segmentPoses[i] += (segmentPoses[i] - segmentPoses[i - 1]).normalized * (targetDist - Vector2.Distance(segmentPoses[i], segmentPoses[i - 1]));
                    }

                    segments[i].transform.position = segmentPoses[i];
                }
            }
        }
    }
}
