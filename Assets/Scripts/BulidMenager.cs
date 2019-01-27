using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulidMenager : MonoBehaviour {

    
    public Camera cam;
    GameObject Model;
    public Material Correct;
    public Material Incorrect;
    BuildMenager.Build Current;
    Budget budget;

    private void Start()
    {
        budget = GameObject.Find("HUD").GetComponent<Budget>();
    }

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
                if (Vector3.Distance(transform.position, HitInfo.point) < 4 && !Physics.CheckBox(new Vector3(Mathf.RoundToInt(HitInfo.point.x), 1, Mathf.RoundToInt(HitInfo.point.z)), Vector3.one * 0.3f, Quaternion.Euler(Vector3.zero)))
                {
                    Model.GetComponent<MeshRenderer>().material = Correct;
                    Model.transform.position = new Vector3(Mathf.RoundToInt(HitInfo.point.x), 1, Mathf.RoundToInt(HitInfo.point.z));
                    if (Input.GetMouseButtonDown(0) && budget.Value>=Current.Cost)
                    {
                        Instantiate(Current.Prefab, Model.transform.position, Quaternion.identity);
                        GameObject.Find("HUD").SendMessage("ChangeValue", -Current.Cost);
                       
                    }
                    //Debug.Log();
                }
                else
                {
                    Model.GetComponent<MeshRenderer>().material = Incorrect;
                    Model.transform.position = new Vector3(Mathf.RoundToInt(HitInfo.point.x), 1, Mathf.RoundToInt(HitInfo.point.z));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0)) { Destroy(Model); this.enabled = false; }
    }
}
