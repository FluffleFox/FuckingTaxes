using UnityEngine;
using System.Collections;

public class Node
{

    public bool walkable;
    public bool wall;
    public bool water;
    public bool electricity;
    public bool gaz;
    public GameObject Child;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;



    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public void SetBool(string name, bool value)
    {
        switch (name)
        {
            case "walkable": { walkable = value; break; }
            case "water": { water = value; break; }
            case "wall": { wall = value; break; }
            case "electricyity": { electricity = value; break; }
            default: { break; }
        }
    }

    public void SetChild(GameObject k)
    {
        Child = k;
        Debug.Log(Child.name);
    }
}