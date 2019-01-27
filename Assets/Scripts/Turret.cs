﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public GameObject mainCanon;
    public GameObject rotateBase;

    public GameObject target;

    private void Update()
    {
        this.LookAtTarget(target.transform.position);
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
            rotateBase.transform.rotation.eulerAngles.y +348f,
            rBOldRot.z));
    }

}
