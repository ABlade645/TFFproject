using UnityEngine;
using System.Collections.Generic;

public class AstarAgent : MonoBehaviour
{
    public AstarAlgorythm grid;
    public Transform target;
    public float speed = 3f;
    public float maxWaitTime;
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
        if (grid.gridExists && path == null)        
            path = computation.FindPath(grid, transform.position, target.position);
            
        if(waitTime <= 0)
        {
            path = computation.FindPath(grid, transform.position, target.position);
            waitTime = maxWaitTime;
        }
            

        if (path == null || index >= path.Count)
            return;       
        
        if(waitTime > 0)
            waitTime -= Time.deltaTime;

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
}