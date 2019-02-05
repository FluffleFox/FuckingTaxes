using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeStack : MonoBehaviour {

    public LayerMask Mask;
    public Mesh[] Pipes;
    Grid grid;
    void Awake()
    {

        grid = GameObject.Find("AStarMenager").GetComponent<Grid>();
        Node MyNode = grid.NodeFromWorldPoint(transform.position);
        MyNode.water = true;
    }

    void Start()
    {
        foreach (Collider k in Physics.OverlapBox(transform.position, Vector3.one * 1.5f, Quaternion.Euler(Vector3.zero), Mask))/*GameObject.FindGameObjectsWithTag("Wall"))*/
        {
            int x = grid.NodeIndexFromWorldPoint(k.transform.position)[0];
            int y = grid.NodeIndexFromWorldPoint(k.transform.position)[1];
            //if (!grid.GetWaterStateByIndex(x, y)) { continue; }
            int connect = 0;
            if (grid.GetWaterStateByIndex(x - 1, y)) { connect += 1; }
            if (grid.GetWaterStateByIndex(x + 1, y)) { connect += 10; }
            if (grid.GetWaterStateByIndex(x, y - 1)) { connect += 100; }
            if (grid.GetWaterStateByIndex(x, y + 1)) { connect += 1000; }

            switch (connect)
            {
                //case 0: { break; }
                case 1:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[0];
                        k.name = "I";
                        break;
                    } // I

                case 10:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[0];
                        k.name = "I";
                        break;
                    } // I

                case 11:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[0];
                        k.name = "I";
                        break;
                    } // I

                case 100:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[1];
                        k.name = "I";
                        break;
                    } // I

                case 101:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh =Pipes[4];
                        k.name = "L";
                        break;
                    } //L

                case 110:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[5];
                        k.name = "L";
                        break;
                    } //L
                case 111:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[6];
                        k.name = "T";
                        break;
                    } //T
                case 1000:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[1];
                        k.name = "I";
                        break;
                    } // I
                case 1001:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[3];
                        k.name = "L";
                        break;
                    } // L
                case 1010:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[2];
                        k.name = "L";
                        break;
                    } // L
                case 1011:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[8];
                        k.name = "T";
                        break;
                    } // T
                case 1100:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[1];
                        k.name = "I";
                        break;
                    } // I
                case 1101:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[9];
                        k.name = "T";
                        break;
                    } // T

                case 1110:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[7];
                        k.name = "T";
                        break;
                    } // T

                case 1111:
                    {
                        k.gameObject.GetComponent<MeshFilter>().mesh = Pipes[10];
                        k.name = "X";
                        break;
                    } // X
            }
        }
        Destroy(this);
    }


}