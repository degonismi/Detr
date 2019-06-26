using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    GameObject GG;
    // Start is called before the first frame update
    void Start()
    {
        GG = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GG != null)
        {
            if (Vector3.Distance(GG.transform.position, transform.position) <= 0.1f)
            {
                Moving.dead = true;
            }
        }
    }
}
