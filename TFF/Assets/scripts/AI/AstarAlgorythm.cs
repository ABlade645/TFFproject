using UnityEngine;

public class AstarAlgorythm : MonoBehaviour
{
    public Vector2 size;
    public LayerMask obstacleMask;
    public bool gridExists;

    public Node[,] grid;

    void Start()
    {
        BuildGrid();
    }

    public void BuildGrid()
    {
        grid = new Node[(int)size.y, (int)size.x];

        for (int y = 0; y < size.y; y++)
            for (int x = 0; x < size.x; x++)
            {
                Vector2 worldPos =
                    new Vector2(x - size.x / 2 + 0.5f, y - size.y / 2 + 0.5f)
                    + (Vector2)transform.position;

                bool walkable = !Physics2D.OverlapCircle(worldPos, 0.45f, obstacleMask);
                grid[y, x] = new Node(worldPos, walkable);
            }
        gridExists = true;
    }

    public Node GetNodeFromWorld(Vector2 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x - transform.position.x + size.x / 2 - 0.5f);
        int y = Mathf.RoundToInt(worldPos.y - transform.position.y + size.y / 2 - 0.5f);

        return grid[y, x];
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(gridExists)
            for (int y = 0;y < size.y; y++)
                for (int x = 0; x < size.x; x++)
                    if (grid[y, x].walkable)                    
                        Gizmos.DrawWireCube(grid[y, x].pos, Vector2.one);
                    
    }
}

public class Node
{
    public Vector2 pos;
    public bool walkable;

    public Node(Vector2 pos, bool walkable)
    {
        this.pos = pos;
        this.walkable = walkable;
    }
}
