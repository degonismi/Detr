using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Detector : MonoBehaviour
{
    public Material Cube_touched;
    GameObject GG,sph;
    bool check,lkm;
    // Start is called before the first frame update
    void Start()
    {
        GG = GameObject.FindGameObjectWithTag("Player");
        sph = GameObject.FindGameObjectWithTag("EditorOnly");
        check = false;
        lkm = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GG != null)
        {
            if (Vector3.Distance(GG.transform.position, transform.position) < 1f)
            {
                Change_mat();
                Init();
            }
        }
    }

    public void Init()
    {
        /*if (Vector3.Distance(GG.transform.position, transform.position) > 1 && !check && Input.GetMouseButtonDown(0))
        {
            lkm = true;
        }
        if (Vector3.Distance(GG.transform.position, transform.position) <= 1 && !check)
        {
            Change_mat();
            if (lkm)
            {
                Moving.mov_left = !Moving.mov_left;
                gameObject.GetComponent<Cube_Detector>().enabled = false;
            }
        }*/
        if (GG.transform.position.y- transform.position.y > 0.5f&& Input.GetMouseButtonDown(0))
        {
            Moving.mov_left = !Moving.mov_left;
            GG.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            sph.SetActive(false);
            sph.SetActive(true);
        }
       
    }
    
    public void Change_mat()
    {
        GetComponent<MeshRenderer>().material = Cube_touched;
        check = true;
    }
}
