using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    Vector3[] path;
    Vector3 Target;
    int WayPoint = 0;
    int I = 0;

    public float Speed=5;
    public float HP=3;

    bool HunterMode = false;
    Transform Player;

    Grid Astar;

	// Use this for initialization
	void Start () {
        Target = GameObject.Find("Sejf").transform.position;
        Astar = GameObject.Find("AstarMenager").GetComponent<Grid>();
        path=Astar.GetPath(transform.position, Target);
        Player = GameObject.Find("Player").transform;

    }
	
	// Update is called once per frame
	void Update () {
        if (HunterMode)
        {
            if (Vector3.Distance(transform.position, Player.position) > 2f)
            { path = Astar.GetPath(transform.position, Player.position); }
        }
        transform.rotation = Quaternion.Euler(-90, 0, Mathf.Atan2(path[WayPoint].y - transform.position.y, path[WayPoint].x - transform.position.x) * Mathf.Rad2Deg);//Quaternion.LookRotation(path[WayPoint] - transform.position);
        Vector3 Translation = path[WayPoint] - transform.position;
        Translation = new Vector3(Translation.x, 0, Translation.z);
        Translation.Normalize();
        transform.Translate(Translation * Speed * Time.deltaTime, Space.World);
		if(Vector3.Distance(transform.position, path[WayPoint]) < 1.1f)
        {
            I++;
            if (I >= path.Length && !HunterMode)
            {
                foreach (GameObject k in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    k.SendMessage("ChangeMode", false);
                }
                Destroy(gameObject);
            }
            else if (I >= path.Length && HunterMode)
            { WayPoint = 0;  path[0] = Player.position; }
            else
            { WayPoint = I; }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            other.transform.parent = this.transform;
            other.GetComponent<Collider>().enabled = false;
            other.transform.localPosition = new Vector3(0, 0, 0.5f);
            foreach (GameObject k in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (k.transform.childCount == 0)
                { k.SendMessage("ChangeMode", true); }
            }
        }
        Target = new Vector3(Mathf.Sign(Random.Range(-1.0f, 1.0f)) * 10, 0, Mathf.Sign(Random.Range(-1.0f, 1.0f)) * 10);
        path = Astar.GetPath(transform.position, Target);
        I = 0;
        WayPoint = 0;
    }

    void GetDmg(float dmg)
    {
        HP -= dmg;
        Debug.Log(HP);
        if (HP <= 0)
        {
            if (transform.childCount > 0)
            {
                transform.GetChild(0).GetComponent<Collider>().enabled = true;
                transform.GetChild(0).position = new Vector3(Mathf.RoundToInt(transform.position.x), 1, Mathf.RoundToInt(transform.position.z));
                transform.GetChild(0).rotation = Quaternion.Euler(Vector3.zero);
                foreach (GameObject k in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    k.SendMessage("Start");
                }
                transform.GetChild(0).parent = this.transform.parent;
            }
            Destroy(gameObject);
        }
    }

    void ChangeMode(bool mode)
    {
        HunterMode = mode;
        if (!HunterMode)
        {
            Target = new Vector3(Mathf.Sign(Random.Range(-1.0f, 1.0f)) * 10, 0, Mathf.Sign(Random.Range(-1.0f, 1.0f)) * 10);
            path = Astar.GetPath(transform.position, Target);
        }
        I = 0;
        WayPoint = 0;
    }
}
