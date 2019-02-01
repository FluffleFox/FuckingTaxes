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

    public LayerMask Mask;

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
        if(Physics.RaycastAll(ray).Length>0)
        {
            Vector3 point = Physics.RaycastAll(ray)[0].point;
            foreach(RaycastHit k in Physics.RaycastAll(ray))
            {
                if (k.collider.tag == "Ground") { point = k.point; break; }
            }
            if (Model != null)
            {
                if (Vector3.Distance(transform.position, point) < 4 && Physics.OverlapSphere(new Vector3(point.x, 1, point.z), 0.3f).Length==0)
                {
                    Model.GetComponent<MeshRenderer>().material = Correct;
                    if (Current.Quantize) { Model.transform.position = new Vector3(Mathf.RoundToInt(point.x), 1, Mathf.RoundToInt(point.z)); }
                    else { Model.transform.position = new Vector3(point.x, 1, point.z); }
                    if (Input.GetMouseButtonDown(0) && budget.Value>=Current.Cost)
                    {
                        Instantiate(Current.Prefab, Model.transform.position, Quaternion.identity);
                        GameObject.Find("HUD").SendMessage("ChangeValue", -Current.Cost);
                       
                    }
                }
                else
                {
                    Model.GetComponent<MeshRenderer>().material = Incorrect;
                    Model.transform.position = new Vector3(point.x, 1, point.z); 
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) { Destroy(Model); this.enabled = false; }
    }
}
