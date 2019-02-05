using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replace : MonoBehaviour {

    public bool walkable;
    Grid grid;
    void Awake()
    {
        grid = GameObject.Find("AStarMenager").GetComponent<Grid>();
        int x = grid.NodeIndexFromWorldPoint(transform.position)[0];
        int y = grid.NodeIndexFromWorldPoint(transform.position)[1];
        Debug.Log(Physics.OverlapBox(transform.position, Vector3.one * 0.3f, Quaternion.Euler(Vector3.zero))[0].name);
        Destroy(Physics.OverlapBox(transform.position, Vector3.one * 0.3f, Quaternion.Euler(Vector3.zero))[0].gameObject);
        GameObject.Find("AStarMenager").GetComponent<Grid>().SetNode(x, y, walkable);
        Destroy(this);
    }

}
