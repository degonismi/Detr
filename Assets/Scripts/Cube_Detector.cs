using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Detector : MonoBehaviour
{
    public Material Cube_touched;
    bool check;
    private float _pos;

    void Start()
    {
        check = false;
        _pos = transform.position.z;
    }

    IEnumerator MoveCube()
    {
        while (Mathf.Abs(_pos-transform.position.z) < 10)
        {
            transform.position = Vector3.MoveTowards(transform.position,  transform.position + new Vector3(1,0,1), 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
    }
    
    public void Change_mat()
    {
        if(GetComponent<CubeVictory>() == null)
        StartCoroutine(MoveCube());
        if(Cube_touched != null)
        GetComponent<MeshRenderer>().material = Cube_touched;
        check = true;
    }
}
