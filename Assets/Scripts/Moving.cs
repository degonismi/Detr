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

    void Start()
    {
        mov_left = false;
        dead = false;
        start = false;
        speed_fail = 0;
        sphere.GetComponent<Animator>().enabled = false;
        _prevPos = (int) transform.position.y;
        _changeDirection = false;
    }
    void Update()
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(transform.position.x, transform.position.y - 3.5f, transform.position.z), 1 * Time.deltaTime);
        // if (Input.GetMouseButtonDown(0)) ChangeDirection();
        if (start) {
            sphere.GetComponent<Animator>().enabled = true;
            start_speed = speed;
            Move_Init();
            //if (dead)
            //{
            //    Dead_Init();
            //}
            //else
            //{
            //    Move_Init();
            //}
        }
        else
        {
            start_speed = 0;
        }

        if (transform.position.y <= _prevPos-1)
        {
            _prevPos = (int)transform.position.y;
            mov_left = _changeDirection;
        }

        

    }
    
    public void Move_Init()
    {
        
        if (mov_left)
        {
            transform.Translate(-speed * Time.deltaTime, -speed * Time.deltaTime, 0);
        }
        else
        {
            transform.Translate(0, -speed * Time.deltaTime, -speed * Time.deltaTime);
        }
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


    public void ChangeDirection()
    {
        _changeDirection = !_changeDirection;
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