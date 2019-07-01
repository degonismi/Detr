using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed;
    public bool mov_left,dead,start;
    public GameObject sphere,part;
    float start_speed, speed_fail;



    public bool _changeDirection;
    public bool IsJump;
    public int JumpStep;

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
        start_speed = speed;
        _audioSource = GetComponent<AudioSource>();
        _animator = sphere.GetComponent<Animator>();
        _playerDeadTrigger = GetComponentInChildren<PlayerDeadTrigger>();
        mov_left = false;
        dead = false;
        start = false;
        IsJump = false;
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
            if (!dead)
            {
                _animator.enabled = true;
                start_speed = speed;
                mov_left = _changeDirection;
                if (transform.position.y <= _prevPos - 0.9f)
                {
                    
                    if (IsJump)
                    {
                        speed = start_speed * 2;
                       // GetNextNodeIfJump(3);
                       //GetNextNode();
                        //StartCoroutine(Jump());
                        //JumpStep--;
                        //_prevPos;
                        //IsJump = false;
                    }
                    else
                    {
                        StopAllCoroutines();
                        speed = start_speed;
                        GetNextNode();
                        StartCoroutine(Run());
                    }



                    //if (IsJump)
                    //{
                    //    if (JumpStep > 1)
                    //    {
                    //        JumpStep--;
                    //    }
                    //    else
                    //    {
                    //        JumpStep = 0;
                    //        IsJump = false;
                    //        GetNextNode();
                    //        speed = 3;
                    //        Jump_Init(3);
                    //        StartCoroutine(Run());
                    //    }
                    //}
                    //else
                    //{

                    //    GetNextNode();
                    //    speed = 3;
                    //    //GetNextNode();
                    //    Move_Init();
                    //}
                    _prevPos--;

                    ////if(!IsJump)


                    if (_animator != null)
                        _animator.SetTrigger("Start");
                    _audioSource.Play();
                }
               // Move_Init();
            }
            

            //dead = _playerDeadTrigger.CheckGround();
            if (!_playerDeadTrigger.CheckGround() && !IsJump)
            {
                Dead_Init();
            }
            
        }
        else
        {
            start_speed = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!start)
            {
                start = true;
                StartCoroutine(Run());
            }
            
        }
        
    }



     IEnumerator Run()
    {
        while (transform.position.y>_prevPos-1)
        {
            transform.position = Vector3.MoveTowards(transform.position, _nextPosition, speed * Time.deltaTime);
            yield return null;
        }

    }

     IEnumerator Jump()
     {
         while (true)
         {
             transform.position = Vector3.MoveTowards(transform.position, _nextPosition, speed * Time.deltaTime);
            yield return null;
         }

         
     }

    //Moving to next node
    public void Move_Init()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextPosition, speed * Time.deltaTime);
    }

    public void Jump_Init(int nodes)
    {
        JumpStep = nodes;
        //int l = ++nodes;
        //IsJump = true;
        //GetNextNodeIfJump(nodes);
        transform.position = Vector3.MoveTowards(transform.position, _nextPosition, speed * Time.deltaTime * nodes);
    }



    public void Dead_Init()
    {
        if (!dead)
        {
            StopAllCoroutines();
            EventManager.instance.OnDeadAction?.Invoke();
            UI.retry_active = true;
            //sphere.SetActive(false);
            part.SetActive(true);
            part.transform.SetParent(null);
            Destroy(_animator);
            GetComponent<Collider>().isTrigger = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;

            //gameObject.GetComponent<MeshRenderer>().enabled = true;
            // speed_fail = Mathf.Lerp(speed_fail, -40, 4 * Time.deltaTime);
            //transform.Translate(0, speed_fail * Time.deltaTime, 0);
            Destroy(gameObject, 3);
        }

        dead = true;

    }

    public void GetNextNodeIfJump(int jumpStep)
    {
        int k = jumpStep;
        _prevPosition = _nextPosition;
        //speed *= k;
        IsJump = true;
        JumpStep = jumpStep;
        // JumpStep;
        if (mov_left)
        {
            _nextPosition = _prevPosition - Vector3.right * k + Vector3.down * k;
        }
        else
        {
            _nextPosition = _prevPosition - Vector3.forward * k + Vector3.down * k;

        }
        //_leftNode = _prevPosition - Vector3.right * k + Vector3.down * k;
        //_rightNode = _prevPosition - Vector3.forward * k + Vector3.down * k;
        Debug.Log(_rightNode + " | "+ _leftNode);
       
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
        //else
        //{
        //    Dead_Init();
        //}
        
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
            dead = true;
            StopAllCoroutines();
            EventManager.instance.OnVictoryAction?.Invoke();
            Debug.Log("Victory");
        }

        if (other.GetComponent<CubeJump>())
        {
            StopAllCoroutines();
            GetNextNodeIfJump(3);
            StartCoroutine(Jump());
            if (!IsJump)
                IsJump = true;
            //J//umpStep = 3;
            Debug.Log("Jump");
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