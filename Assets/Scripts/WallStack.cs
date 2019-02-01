using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStack : MonoBehaviour {


    public LayerMask Mask;
    public Mesh[] Walls;
    Grid grid;
    void Awake () {

        grid = GameObject.Find("AStarMenager").GetComponent<Grid>();
        int x = grid.NodeIndexFromWorldPoint(transform.position)[0];
        int y = grid.NodeIndexFromWorldPoint(transform.position)[1];
        GameObject.Find("AStarMenager").GetComponent<Grid>().SetNode(x, y, false);

    }

	void Start () {
        foreach (Collider k in Physics.OverlapBox(transform.position, Vector3.one*1.5f, Quaternion.Euler(Vector3.zero), Mask))/*GameObject.FindGameObjectsWithTag("Wall"))*/
        {
            int x = grid.NodeIndexFromWorldPoint(k.transform.position)[0];
            int y = grid.NodeIndexFromWorldPoint(k.transform.position)[1];
            int connect = 0;
            if (grid.GetStateByIndex(x - 1, y)) { connect+=1; }
            if (grid.GetStateByIndex(x + 1, y)) { connect+=10; }
            if (grid.GetStateByIndex(x, y - 1)) { connect+=100; }
            if (grid.GetStateByIndex(x, y + 1)) { connect+=1000; }

            switch (connect)
            {
                //case 0: { break; }
                case 1:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[0];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[0];
                        k.transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    } // I

                case 10:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[0];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[0];
                        k.transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    } // I

                case 11:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[0];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[0];
                        k.transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    } // I

                case 100:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[0];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[0];
                        k.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    } // I

                case 101:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[1];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[1];
                        k.transform.rotation = Quaternion.Euler(0, -90, 0);
                        break;
                    } //L

                case 110:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[1];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[1];
                        k.transform.rotation = Quaternion.Euler(0, 180, 0);
                        break;
                    } //L
                case 111:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[2];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[2];
                        k.transform.rotation = Quaternion.Euler(0, 180, 0);
                        break;
                    } //T
                case 1000:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[0];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[0];
                        k.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    } // I
                case 1001:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[1];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[1];
                        k.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    } // L
                case 1010:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[1];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[1];
                        k.transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    } // L
                case 1011:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[2];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[2];
                        k.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    } // T
                case 1100:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[0];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[0];
                        k.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    } // I
                case 1101:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[2];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[2];
                        k.transform.rotation = Quaternion.Euler(0, 270, 0);
                        break;
                    } // T

                case 1110:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[2];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[2];
                        k.transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    } // T

                case 1111:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Walls[3];
                        k.gameObject.GetComponent<MeshCollider>().sharedMesh = Walls[3];
                        k.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    } // X
            }
        }
        Destroy(this);
    }


}
