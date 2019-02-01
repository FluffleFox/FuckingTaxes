using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour {

    public float DMG;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        { collision.gameObject.SendMessage("GetDmg", DMG*Mathf.RoundToInt(GetComponent<Rigidbody>().velocity.magnitude)); }
    }
}
