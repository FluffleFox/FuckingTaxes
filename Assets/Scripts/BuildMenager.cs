using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenager : MonoBehaviour {

    [System.Serializable]
    public struct Build
    {
        public GameObject Model;
        public GameObject Prefab;
        public int Cost;
        public bool Quantize;
    }

    public Build[] TurretArray;
    public Build CurrentBuild;

    public void Select(int ID)
    {
        CurrentBuild = TurretArray[ID];
        GameObject.Find("Player").GetComponent<BulidMenager>().enabled = true;
    }


}
