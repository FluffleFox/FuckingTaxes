using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody RB;
    public float speed = 5f;
    float root;
    Vector3 SavePos;
    GameObject Sejf;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        Sejf = GameObject.Find("Sejf");
        SavePos = GameObject.Find("Sejf").transform.position;
    }


    void Update () {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float angle = Mathf.RoundToInt(Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg /90f)*90f;
            transform.rotation = Quaternion.Euler(0, angle,0);
            RB.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
            GetComponent<Animator>().SetBool("walk", true);
        }
        else { GetComponent<Animator>().SetBool("walk", false); }
        if (Sejf == null) { Debug.Log("GameOver"); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            other.transform.position = SavePos;
            foreach (GameObject k in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                k.SendMessage("Start");
            }
        }
        if (other.tag == "Money")
        {
            GameObject.Find("HUD").SendMessage("ChangeValue", int.Parse(other.name));
            Destroy(other.gameObject);
        }
    }
}
