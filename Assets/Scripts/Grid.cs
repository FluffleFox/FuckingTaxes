using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{

    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Awake()
    {
        nodeDiameter = nodeRadius;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckBox(worldPoint, new Vector3(0.45f, 2f, 0.45f), Quaternion.Euler(0, 0, 0), unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }


    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = ((worldPosition.x - 0.5f) + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = ((worldPosition.z - 0.5f) + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
       /* if (grid[x, y].walkable)
        { return grid[x, y]; }
        else
        {
            float dist = Mathf.Infinity;
            Node ret = grid[x, y];
            foreach (Node k in GetNeighbours(grid[x, y]))
            {
                if (k.walkable && Vector3.Distance(worldPosition, k.worldPosition) < dist)
                {
                    dist = Vector3.Distance(worldPosition, k.worldPosition);
                    ret = k;
                }
            }
            return ret;
        }*/
    }

    public List<Node> path;
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.color = (n.water) ? Color.blue : Color.white;
                if (path != null)
                    if (path.Contains(n))
                        Gizmos.color = Color.black;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }

    public Vector3 GetPath(Vector3 Start, Vector3 End)
    {
        GetComponent<PathFinding>().FindPath(Start, End);
        if (path.Count >= 1)
        { return path[0].worldPosition; }
        else return End;
    }




    public int[] NodeIndexFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = ((worldPosition.x - 0.5f) + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = ((worldPosition.z - 0.5f) + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        int[] ret = { x, y };
        return ret;
    }

    public bool GetWallStateByIndex(int x, int y)
    {
        if (x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY)
        {
            return grid[x, y].wall;
        }
        else return false;
    }

    public bool GetWalkableStateByIndex(int x, int y)
    {
        if (x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY)
        {
            return !grid[x, y].walkable;
        }
        else return false;
    }

    public bool GetWaterStateByIndex(int x, int y)
    {
        if (x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY)
        {
            return grid[x, y].water;
        }
        else return false;
    }

    public bool GetGazStateByIndex(int x, int y)
    {
        if (x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY)
        {
            return grid[x, y].gaz;
        }
        else return false;
    }




    public void SetNode(int x, int y, bool status)
    {
        grid[x, y].walkable = status;
    }
}