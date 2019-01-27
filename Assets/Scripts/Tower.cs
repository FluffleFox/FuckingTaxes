using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public float Reload;
    float reload;
    public float DMG;
    public float BackStep;
    public float Range;
    Transform Target;

    private void Start()
    {
        reload = Reload;
    }

    void Update () {
        if (Target == null)
        {
            Collider[] Obj = Physics.OverlapSphere(transform.position, Range);
            foreach(Collider k in Obj)
            {
                if (k.tag == "Enemy") { Target = k.transform; }
            }
        }
        else
        {
            RaycastHit Hitinfo;
            if (Physics.Raycast(new Ray(transform.position, Target.position-transform.position), out Hitinfo, Range))
            {
                if (Hitinfo.collider.gameObject == Target.gameObject && reload <= 0)
                { Target.SendMessage("GetDmg", DMG); reload = Reload; }
            }
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(Target.position-transform.position), 5 * Time.deltaTime);
            if (reload > 0) { reload -= Time.deltaTime; }
            if (Vector3.Distance(transform.position, Target.position) > Range) { Target = null; }
        }
	}
}
