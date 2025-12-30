using UnityEngine;
using System.Collections.Generic;

public class AstarAgent : MonoBehaviour
{
    [Header("General")]
    public AstarAlgorythm grid;
    public Transform target;
    public float speed = 3f;
    public float maxWaitTime;
    public LayerMask whatToExtrude;

    [Header("Stopping distance")]
    public bool useStopDistance;
    public int stopDist;

    float waitTime;

    AstarFollow computation;

    List<Node> path = null;
    int index = 0;

    void Start()
    {
        computation = grid.GetComponent<AstarFollow>();
        waitTime = maxWaitTime;
    }

    void Update()
    {
        if (waitTime > 0)
            waitTime -= Time.deltaTime;

        if (waitTime <= 0 && grid.gridExists)      
            Recalculate();

        if(useStopDistance)
            if (path == null || index >= stopDist)
            {
              Recalculate();
              return;
            }
        else
            if (path == null || index >= path.Count)
            {
                Recalculate();
                return;
            }


        Vector2 targetPos = path[index].pos;
        transform.position = Vector2.MoveTowards(
            transform.position, targetPos, speed * Time.deltaTime);

        if ((Vector2)transform.position == targetPos)
            index++;
    }

    void OnDrawGizmos()
    {
        if (path == null) 
            return;

        Gizmos.color = Color.blue;
        for (int i = 0; i < path.Count - 1; i++)
            Gizmos.DrawLine(path[i].pos, path[i + 1].pos);
    }

    void Recalculate()
    {       
        index = 0;
        if(useStopDistance)
            path = computation.FindPath(grid, transform.position, transform.position + (target.position - transform.position).normalized * ((target.position - transform.position).magnitude - stopDist), whatToExtrude, gameObject);
        else
            path = computation.FindPath(grid, transform.position, target.position, whatToExtrude, gameObject);
        waitTime = maxWaitTime;
    }
}