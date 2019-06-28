using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private Transform _targetTransform;

    private void Start()
    {
        _transform = gameObject.transform;
    }

    private void Update()
    {
        if(_targetTransform!=null)
        _transform.position = Vector3.Lerp(_transform.position, _targetTransform.position, Time.deltaTime);
    }
}
