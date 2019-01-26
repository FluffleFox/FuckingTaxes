using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulidMenager : MonoBehaviour {

    
    public Camera cam;
    GameObject Model;
    BuildMenager.Build Current;

    private void OnEnable()
    {
        Current = GameObject.Find("HUD").GetComponent<BuildMenager>().CurrentBuild;
        GameObject GO = (GameObject)Instantiate(Current.Model, new Vector3(0, -1, 0), Quaternion.identity);
        Model = GO;
    }


    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit HitInfo;
        if (Physics.Raycast(ray, out HitInfo))
        {
            if (Model != null)
            {
                if (Vector3.Distance(transform.position, HitInfo.point) < 5)
                {
                    Model.GetComponent<MeshRenderer>().enabled = true;
                    Model.transform.position = new Vector3(Mathf.RoundToInt(HitInfo.point.x), 1, Mathf.RoundToInt(HitInfo.point.z));
                    if (Input.GetMouseButtonDown(0))
                    { Instantiate(Current.Prefab, Model.transform.position, Quaternion.identity);/*Budget-=BuildManager.currentBuild.cost;*/ }
                }
                else
                {
                    Model.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0)) { Destroy(Model); this.enabled = false; }
    }
}
