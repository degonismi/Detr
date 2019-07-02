using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    private Moving _moving;

    private void Start()
    {
        _moving = FindObjectOfType<Moving>();
        EventManager.instance.OnDeadAction += StopFollowingWithDelay;
        EventManager.instance.OnJumpAction += CameraAnimation;
    }

    private void OnDestroy()
    {
        EventManager.instance.OnDeadAction -= StopFollowingWithDelay;
        EventManager.instance.OnJumpAction -= CameraAnimation;
    }

    private void Update()
    {
        if(_targetTransform)
            transform.position = Vector3.Lerp(transform.position, _targetTransform.position, Time.deltaTime * _moving.speed);
            //transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position, Time.deltaTime * _moving.speed);
    }

    private void StopFollowing()
    {
        _targetTransform = null;
    }

    private void StopFollowingWithDelay()
    {
        Invoke("StopFollowing",1f );
    }

    private void CameraAnimation()
    {
        GetComponent<Animator>().SetTrigger("Start");
    }

}
