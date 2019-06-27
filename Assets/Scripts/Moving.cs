using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed;
    public static bool mov_left,dead,start;
    public GameObject sphere,part,camera;
    float start_speed, speed_fail;

    public bool _changeDirection;

    private int _prevPos;
    private float _curPos;

    private Vector3 _pos;
    [SerializeField] private Vector3 _prevPosition;
    [SerializeField] private Vector3 _nextPosition;

    private Vector3 _leftNode, _rightNode;

    void Start()
    {
        mov_left = false;
        dead = false;
        start = false;
        speed_fail = 0;
        sphere.GetComponent<Animator>().enabled = false;
        _prevPos = (int)transform.position.y;
        _changeDirection = false;
        _nextPosition = transform.position;
        GetNextNode();
    }
    

    void Update()
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(transform.position.x, transform.position.y - 3.5f, transform.position.z),  Time.deltaTime);
        // if (Input.GetMouseButtonDown(0)) ChangeDirection();
        if (start) {
            sphere.GetComponent<Animator>().enabled = true;
            start_speed = speed;
            mov_left = _changeDirection;
            //Move_Init();

            if (dead)
            {
                Dead_Init();
            }
            else
            {
                Move_Init();
            }
        }
        else
        {
            start_speed = 0;
        }

        if (transform.position.y <= _prevPos-0.75f)
        {
            _prevPos--;
            GetNextNode();
        }
    }
    
    //Moving to next node
    public void Move_Init()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextPosition, speed*Time.deltaTime);
    }

    public void Dead_Init()
    {
        UI.retry_active = true;
        sphere.SetActive(false);
        part.SetActive(true);
        //gameObject.GetComponent<MeshRenderer>().enabled = true;
        speed_fail = Mathf.Lerp(speed_fail, -40, 4 * Time.deltaTime);
        transform.Translate(0, speed_fail * Time.deltaTime, 0);
        Destroy(gameObject, 1);
    }


    //Get node for next move
    public void GetNextNode()
    {
        _prevPosition = _nextPosition;
        _leftNode = _prevPosition - Vector3.right + Vector3.down;
        _rightNode = _prevPosition - Vector3.forward + Vector3.down;
        if (mov_left)
        {
            _nextPosition = _prevPosition - Vector3.right + Vector3.down;
        }
        else
        {
            _nextPosition = _prevPosition - Vector3.forward + Vector3.down;
            
        }
    }

    public void ChangeDirection()
    {
        _changeDirection = !_changeDirection;
        mov_left = _changeDirection;
        if (mov_left)
        {
            _nextPosition = _leftNode;
        }
        else
        {
            _nextPosition = _rightNode;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Cube_Detector>())
        {
            other.GetComponent<Cube_Detector>().Change_mat();
        }
        else if(other.GetComponent<Dead>())
        {
            Dead_Init();
        }
    }


}