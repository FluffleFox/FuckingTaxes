using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request : MonoBehaviour
{

    [System.Serializable]
    public struct Condition
    {
        public enum RequestType { Name, Wall, Water, Electricity, Gaz, Distance, Collider};
        public RequestType Type;
        public string Conditions;
    }

    public Condition[] Conditions;
    public Material Correct;
    public Material Incorrect;
    public bool Quantize;
    public LayerMask Mask;
    Node MyNode;

    [HideInInspector]
    public bool Possible;
    [HideInInspector]
    public Vector3 Position;
    [HideInInspector]
    public Quaternion Rotation;

    Transform Player;
    Renderer Rend;

    private void Start()
    {
        Player = GameObject.Find("Player").transform;
        Rend = GetComponent<Renderer>();
    }




    void Update()
    {
        Possible = true;
        if (Quantize) { transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), 1, Mathf.RoundToInt(transform.position.z)); }
        MyNode = GameObject.Find("AStarMenager").GetComponent<Grid>().NodeFromWorldPoint(transform.position);
        Position = transform.position;
        Rotation = transform.rotation;


        foreach (Condition k in Conditions)
        {
            if (!Possible) { break; }
            switch (k.Type)
            {
                case Condition.RequestType.Name:
                    {
                        if (MyNode.Child != null)
                        {
                            if (MyNode.Child.name != k.Conditions)
                            { Debug.Log("Zła nazwa"); Possible = false; }
                            transform.rotation = MyNode.Child.transform.rotation;
                        }
                        else { Debug.Log("Brak Obiektu"); Debug.Log("X: " + MyNode.gridX + "\t Y: " + MyNode.gridY); Possible = false; }
                        break;
                    }
                case Condition.RequestType.Wall:
                    {
                        if (MyNode.wall != ToBoolean(k.Conditions))
                        { Possible = false; }
                        break;
                    }
                case Condition.RequestType.Water:
                    {
                        if (MyNode.water!=ToBoolean(k.Conditions))
                        { Possible = false; }
                        break;
                    }
                case Condition.RequestType.Electricity:
                    {
                        if (MyNode.electricity != ToBoolean(k.Conditions))
                        { Possible = false; }
                        break;
                    }
                case Condition.RequestType.Gaz:
                    {
                        if (MyNode.gaz != ToBoolean(k.Conditions))
                        { Possible = false; }
                        break;
                    }
                case Condition.RequestType.Collider:
                    {
                        if (Physics.OverlapBox(new Vector3(transform.position.x, 1.5f, transform.position.z), new Vector3(0.4f, 1.8f, 0.4f), transform.rotation, Mask).Length != 0)
                        { Possible = ToBoolean(k.Conditions); }
                        break;
                    }
                case Condition.RequestType.Distance:
                    {
                        if(Vector3.Distance(transform.position, Player.position) > int.Parse(k.Conditions))
                        {
                            Possible = false;
                        }
                        break;
                    }
            }
        }


        if (Possible)
        {
            Rend.material = Correct;
        }
        else { Rend.material = Incorrect; }
    }

    bool ToBoolean(string value)
    {
        switch (value.ToLower())
        {
            case "true":
                return true;
            case "t":
                return true;
            case "1":
                return true;
            case "0":
                return false;
            case "false":
                return false;
            case "f":
                return false;
            default: return false;
        }
    }
}
