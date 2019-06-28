using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadTrigger : MonoBehaviour
{

    public bool CheckGround()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
        bool _grounded = false;
        foreach (var col in colliders)
        {
            if (col.GetComponent<Cube_Detector>())
            {
                _grounded = true;
            }
        }

        return _grounded;
    }


}
