using System.Collections.Generic;
using UnityEngine;

public class AstarFollow : MonoBehaviour
{
    public float obstacleExtruidingDist;
    public bool useExtruding;

    public List<Node> FindPath(AstarAlgorythm grid,Vector2 startWorld,Vector2 targetWorld, LayerMask obstacle, GameObject agent)
    {
        Node start = grid.GetNodeFromWorld(startWorld);
        Node target = grid.GetNodeFromWorld(targetWorld);

        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();

        Dictionary<Node, float> gCost = new Dictionary<Node, float>();
        Dictionary<Node, Node> parent = new Dictionary<Node, Node>();

        open.Add(start);
        gCost[start] = 0;
        parent[start] = null;

        while (open.Count > 0)
        {
            Node current = GetLowestF(open, gCost, target);
            open.Remove(current);
            closed.Add(current);

            if (current == target)
                return ReconstructPath(parent, current);

            foreach (Node neighbour in GetNeighbours(grid, current))
            {
                if (!neighbour.walkable || closed.Contains(neighbour))
                    continue;

                float tentativeG =
                    gCost[current] + Vector2.Distance(current.pos, neighbour.pos);

                if (!gCost.ContainsKey(neighbour) || tentativeG < gCost[neighbour])
                {
                    gCost[neighbour] = tentativeG;
                    parent[neighbour] = current;

                    if (!open.Contains(neighbour))
                    {
                        if (useExtruding)
                        {
                            //if (!Physics2D.OverlapCircle(neighbour.pos, obstacleExtruidingDist, obstacle) && Physics2D.OverlapCircle(neighbour.pos, obstacleExtruidingDist, obstacle) != agent)
                                open.Add(neighbour);
                        }
                        else
                            open.Add(neighbour);
                    }                    
                }
            }
        }

        return closed; 
    }

     Node GetLowestF(List<Node> open,
                           Dictionary<Node, float> gCost,
                           Node target)
    {
        Node best = open[0];
        float bestF = gCost[best] + Heuristic(best, target);

        foreach (Node n in open)
        {
            float f = gCost[n] + Heuristic(n, target);
            if (f < bestF)
            {
                bestF = f;
                best = n;
            }
        }
        return best;
    }

     float Heuristic(Node a, Node b)
    {
        return Vector2.Distance(a.pos, b.pos);
    }

     List<Node> GetNeighbours(AstarAlgorythm grid, Node node)
    {
        List<Node> result = new List<Node>();

        int cx = Mathf.RoundToInt(node.pos.x - grid.transform.position.x + grid.size.x / 2 - 0.5f);
        int cy = Mathf.RoundToInt(node.pos.y - grid.transform.position.y + grid.size.y / 2 - 0.5f);

        for (int y = -1; y <= 1; y++)
            for (int x = -1; x <= 1; x++)
            {
                if (x == 0 && y == 0) continue;

                int nx = cx + x;
                int ny = cy + y;

                if (nx < 0 || ny < 0 || nx >= grid.size.x || ny >= grid.size.y)
                    continue;

                result.Add(grid.grid[ny, nx]);
            }

        return result;
    }

     List<Node> ReconstructPath(Dictionary<Node, Node> parent, Node end)
    {
        List<Node> path = new List<Node>();
        Node current = end;

        while (current != null)
        {
            path.Add(current);
            current = parent[current];
        }

        path.Reverse();
        return path;
    }
}