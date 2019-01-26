using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody RB;
    public float speed = 5f;
    float root;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }


    void Update () {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float angle = Mathf.RoundToInt(Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg /90f)*90f;
            transform.rotation = Quaternion.Euler(-90, 0, angle);
            RB.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
        }
    }
}
