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

	// Use this for initialization
	void Start () {
        Target = GameObject.Find("Sejf").transform.position;
        path = GameObject.Find("AstarMenager").GetComponent<Grid>().GetPath(transform.position, Target);
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(-90, 0, Mathf.Atan2(path[WayPoint].y - transform.position.y, path[WayPoint].x - transform.position.x) * Mathf.Rad2Deg);//Quaternion.LookRotation(path[WayPoint] - transform.position);
        Vector3 Translation = path[WayPoint] - transform.position;
        Translation = new Vector3(Translation.x, 0, Translation.z);
        Translation.Normalize();
        transform.Translate(Translation * Speed * Time.deltaTime, Space.World);
		if(Vector3.Distance(transform.position, path[WayPoint]) < 1.1f)
        {
            I++;
            if (I >= path.Length)
            { Destroy(gameObject); }
            WayPoint = I;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            other.transform.parent = this.transform;
            other.GetComponent<Collider>().enabled = false;
            other.transform.localPosition = new Vector3(0, 0, 0.5f);
        }
        Target = new Vector3(Mathf.Sign(Random.Range(-1.0f, 1.0f)) * 10, 0, Mathf.Sign(Random.Range(-1.0f, 1.0f)) * 10);
        path = GameObject.Find("AstarMenager").GetComponent<Grid>().GetPath(transform.position, Target);
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
}
