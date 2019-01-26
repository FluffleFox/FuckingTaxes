using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {

    Transform Player;
    Vector3 Correct = new Vector3(-5.5f, 7, -5.5f);
    public float Speed = 5f;
	void Start () {
        Player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate((Player.position + Correct - transform.position) * Speed * Time.deltaTime, Space.World);
	}
}
