using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyV2 : MonoBehaviour {

    public float Speed = 3;
    public float HP = 3;
    public int Reward = 50;
    public GameObject RewardModel;


    Transform Player;
    Transform Sejf;
    Grid PathFinder;

    Vector3 Target;
    Vector3 LastTarget;
    Vector3 Evac;
    Vector3 Translation;

	
	void Start () {
        Player = GameObject.Find("Player").transform;
        Sejf = GameObject.Find("Sejf").transform;
        PathFinder = GameObject.Find("AStarMenager").GetComponent<Grid>();
        Evac = transform.position;
        GetComponent<Animator>().SetBool("walk", true);
        Target = Evac;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, Target) < 0.5f)
        {
            LastTarget = Target;
            if (Sejf != null)
            {
                if (Sejf.parent == this.transform.parent) //Sejf stoi luzem;
                { Target = PathFinder.GetPath(Target, Sejf.position); Target.y = transform.position.y; }
                else if(transform.GetChild(0).name!="Sejf")//Sejf jest niesiony Wszyscy do gracza;
                { Target = PathFinder.GetPath(Target, Player.position); Target.y = transform.position.y; }
                else
                {
                    Target = PathFinder.GetPath(Target, Evac); Target.y = transform.position.y;
                    if (Vector3.Distance(transform.position, Evac) < 0.5f)
                    { Destroy(gameObject); }
                }
            }
            else //Sejfu nie ma. Do wyjścia i koniec gry;
            {
                Target = PathFinder.GetPath(Target, Evac); Target.y = transform.position.y;
                if (Vector3.Distance(transform.position, Evac) < 0.5f)
                { Destroy(gameObject); }
            }
            transform.rotation = Quaternion.Euler(0, Mathf.RoundToInt(Mathf.Atan2(LastTarget.x - Target.x, LastTarget.z - Target.z) * Mathf.Rad2Deg / 45.0f) * 45.0f +90f, 0);
        }
        Translation = Target - transform.position;
        Translation.y = 0;
        Translation.Normalize();
        Translation *= Speed;
        transform.Translate(Translation * Time.deltaTime, Space.World);

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            other.transform.parent = this.transform;
            other.transform.localPosition = new Vector3(0, 1.5f, 0);
            other.transform.localRotation = Quaternion.Euler(new Vector3(180,0,-90));
            other.enabled = false;
            other.transform.SetAsFirstSibling();
        }
    }

    void GetDmg(float dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            if(transform.Find("Sejf")!=null)
            //if (transform.GetChild(0).name == "Sejf")
            {
                Transform k = transform.Find("Sejf");
                /*transform.GetChild(0)*/k.position = new Vector3(Mathf.RoundToInt(transform.position.x), 1, Mathf.RoundToInt(transform.position.z));
                /*transform.GetChild(0)*/k.GetComponent<Collider>().enabled = true;
                /*transform.GetChild(0)*/k.rotation = Quaternion.Euler(Vector3.zero);
                /*transform.GetChild(0)*/k.parent = this.transform.parent;
            }
            GameObject GO = (GameObject)Instantiate(RewardModel, transform.position, Quaternion.identity);
            GO.name = Reward.ToString();
            Destroy(GO, 3f);
            Destroy(gameObject);
        }
    }
}
