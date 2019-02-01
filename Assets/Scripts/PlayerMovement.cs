using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody RB;
    public float speed = 5f;
    float root;
    Vector3 SavePos;
    GameObject Sejf;

    public LayerMask mask;
    Transform Target;

    public GameObject Ammo;
    Rigidbody AmmoRB;
    Collider AmmoColl;
    public float Range = 4f;
    public float Reload = 1f;
    public float Force = 10f;
    float reload;


    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        AmmoRB = Ammo.GetComponent<Rigidbody>();
        AmmoColl = Ammo.GetComponent<Collider>();
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
        if (Sejf == null) { Destroy(this); GameObject.Find("HUD").SendMessage("GameOver"); }

        Collider[] obj = Physics.OverlapBox(transform.position + transform.right * 4f *Mathf.Sqrt(2f), new Vector3(4, 4, 4), Quaternion.Euler(0, 45, 0), mask);
        float Dist = Mathf.Infinity;
        Target = null;
        foreach (Collider k in obj)
        {
            if(Vector3.Distance(transform.position, k.transform.position) < Dist)
            { Target = k.transform; Dist = Vector3.Distance(transform.position, k.transform.position); }
        }
        if (Target != null)
        {
            RaycastHit Hitinfo;
            if (Physics.Raycast(new Ray(transform.position, Target.position - transform.position), out Hitinfo, Range*2))
            {
                if (Hitinfo.collider.gameObject.tag == "Enemy" && reload <= 0)
                {
                    Debug.Log("Throw");
                    Target = Hitinfo.collider.gameObject.transform;
                    Ammo.transform.position = transform.position + transform.right*0.5f;
                    Ammo.transform.rotation = Quaternion.Euler(0, Mathf.Atan2(transform.position.x + (transform.right * 0.5f).x - Target.position.x, transform.position.z+(transform.right * 0.5f).z - Target.position.z) * Mathf.Rad2Deg + 90f, 15f);
                    AmmoColl.enabled = true;
                    AmmoRB.isKinematic = false;
                    AmmoRB.AddForce(Ammo.transform.right * Force, ForceMode.Impulse);
                    AmmoRB.AddTorque(new Vector3(0, 0, -1), ForceMode.Impulse);
                    reload = Reload;
                }
            }
        }
        if (reload > 0) { reload -= Time.deltaTime; }
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
