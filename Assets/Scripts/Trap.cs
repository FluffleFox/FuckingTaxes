using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public float DmgPerSec;
    public LayerMask Mask;

    private void Update()
    {
        foreach (Collider k in Physics.OverlapBox(transform.position, Vector3.one * 0.5f, Quaternion.Euler(0, 0, 0), Mask))
        {
            k.SendMessage("GetDmg", DmgPerSec*Time.deltaTime);
        }
    }
}
