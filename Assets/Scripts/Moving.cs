using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed;
    public bool mov_left,dead,start;
    public GameObject sphere,part,camera;
    float start_speed, speed_fail;

    public bool _changeDirection;

    private int _prevPos;
    private float _curPos;

    private Vector3 _pos;
    private Vector3 _prevPosition;
    private Vector3 _nextPosition;

    private Vector3 _leftNode, _rightNode;

    private PlayerDeadTrigger _playerDeadTrigger;

    private AudioSource _audioSource;
    private Animator _animator;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = sphere.GetComponent<Animator>();
        _playerDeadTrigger = GetComponentInChildren<PlayerDeadTrigger>();
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

        if (start)
        {
            //if (_animator != null) 
            
            if (!dead)
            {
                _animator.enabled = true;
                start_speed = speed;
                mov_left = _changeDirection;
                Move_Init();
                if (transform.position.y <= _prevPos - 0.75f)
                {
                    _prevPos--;
                    GetNextNode();
                    if (_animator != null)
                        _animator.SetTrigger("Start");
                    _audioSource.Play();
                }
            }
            

            //dead = _playerDeadTrigger.CheckGround();
            if (!_playerDeadTrigger.CheckGround())
            {
                Dead_Init();
            }
            
        }
        else
        {
            start_speed = 0;
        }

        
    }
    
    

    //Moving to next node
    public void Move_Init()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextPosition, speed*Time.deltaTime);
    }

    public void Dead_Init()
    {
        if (!dead)
        {
            UI.retry_active = true;
            //sphere.SetActive(false);
            part.SetActive(true);
            part.transform.SetParent(null);
            Destroy(_animator);
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;

            //gameObject.GetComponent<MeshRenderer>().enabled = true;
            // speed_fail = Mathf.Lerp(speed_fail, -40, 4 * Time.deltaTime);
            //transform.Translate(0, speed_fail * Time.deltaTime, 0);
            Destroy(gameObject, 3);
        }

        dead = true;

    }
    

    public void GetNextNode()
    {
        if (_playerDeadTrigger.CheckGround())
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
        else
        {
            Dead_Init();
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

        if (other.GetComponent<CubeVictory>())
        {
            Debug.Log("Victory");
        }
        //else if(other.GetComponent<Dead>())
        //{
        //    Dead_Init();
        //}
    }

    private void PlaySFX()
    {

    }

}