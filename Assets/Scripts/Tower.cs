using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public float Reload;
    float reload;
    public float DMG;
    public float Range;
    public GameObject mainCanon;
    public GameObject rotateBase;
    Transform Target;
    public ParticleSystem particle;

    private void Start()
    {
        reload = Reload;
        //particle = GetComponentInChildren<ParticleSystem>();
        particle.Pause();
    }


    void Update () {

        float Dist=Mathf.Infinity;
        Collider[] Obj = Physics.OverlapSphere(transform.position, Range);
        foreach (Collider k in Obj)
        {
            if (k.tag == "Enemy")
            {
                if (Vector3.Distance(k.transform.position, transform.position) < Dist)
                {
                    Dist = Vector3.Distance(k.transform.position, transform.position);
                    Target = k.gameObject.transform;
                }
            }
        }

        if (Target != null)
        {
            RaycastHit Hitinfo;
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out Hitinfo, Range))
            {
                if (Hitinfo.collider.gameObject.tag == "Enemy")
                {
                    particle.Play();
                    Debug.Log("Particle");
                    Target = Hitinfo.collider.gameObject.transform;
                    if (reload <= 0)
                    {
                        Target.SendMessage("GetDmg", DMG);
                        reload = Reload;
                    }
                }
            }
            this.LookAtTarget(Target.transform.position);
            if (reload > 0) { reload -= Time.deltaTime; }
            if (Vector3.Distance(transform.position, Target.position) > Range) { Target = null; }
        }
        else { particle.Stop(); Debug.Log("Stop"); }
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);
    }




    public void LookAtTarget(Vector3 target)
    {
        Vector3 mainCanonOldRot = mainCanon.transform.rotation.eulerAngles;
        mainCanon.transform.LookAt(target, transform.up);
        mainCanon.transform.rotation = Quaternion.Euler(new Vector3(
            mainCanon.transform.rotation.eulerAngles.x /*- 93f*/,
            mainCanonOldRot.y,
            mainCanonOldRot.z));
        //mainCanon.transform.rotation = Quaternion.Euler(new Vector3(mainCanon.transform.rotation.x, rot.y, rot.z));

        Vector3 rBOldRot = rotateBase.transform.rotation.eulerAngles;
        rotateBase.transform.LookAt(target, transform.up);
        rotateBase.transform.rotation = Quaternion.Euler(new Vector3(
            rBOldRot.x,
            rotateBase.transform.rotation.eulerAngles.y + 348f,
            rBOldRot.z));
    }
}
