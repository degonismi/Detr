using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed;
    public bool mov_left,dead,start, CanChange;
    public GameObject sphere,part;
    float start_speed, speed_fail;



    public bool _changeDirection;
    public int JumpStep;

    private int _prevPos;
    private float _curPos;
    [SerializeField] private float _timerForInput;

    private Vector3 _pos;
    private Vector3 _prevPosition;
    private Vector3 _nextPosition;


    private Vector3 _leftNode, _rightNode;

    private PlayerDeadTrigger _playerDeadTrigger;

    private AudioSource _audioSource;
    private Animator _animator;

    private Coroutine _inputDelayCoroutine;
    private Coroutine _movingCoroutine;

    void Start()
    {
        start_speed = speed;
        _audioSource = GetComponent<AudioSource>();
        _animator = sphere.GetComponent<Animator>();
        _playerDeadTrigger = GetComponentInChildren<PlayerDeadTrigger>();
        mov_left = false;
        dead = false;
        start = false;
        sphere.GetComponent<Animator>().enabled = false;
        _prevPos = (int)transform.position.y;
        _changeDirection = false;
        _nextPosition = transform.position;
        GetNextNode();
        CanChange = true;

    }
    

    void Update()
    {
        if (_timerForInput > 0)
        {
            _timerForInput -= Time.deltaTime;
        }
        if (start)
        {
            if (!dead)
            {
                _animator.enabled = true;
                mov_left = _changeDirection;
                if (transform.position.y <= _prevPos - 0.85f)
                {
                    
                    //if (!_playerDeadTrigger.CheckGround())
                    //{
                    //   Dead_Init();
                    //}
                    //else
                    //{
                    //if (_movingCoroutine!=null)
                    //{
                    //if (_movingCoroutine != null)
                    //    StopCoroutine(_movingCoroutine);
                    //    _movingCoroutine = null;
                    StopAllCoroutines();
                        GetNextNode();
                        if (JumpStep > 0)
                        {

                            //speed = 9;
                            Debug.Log("-"+ JumpStep);
                            JumpStep--;
                        }
                        else
                        {
                            speed = 3;
                            if (_animator != null)
                                _animator.SetTrigger("Start");

                        }

                        StartCoroutine(Run());

                        _prevPos--;
                        _audioSource.Play();
                    
                }
            }
            
        }
    }

    IEnumerator InputDelay()
    {
        yield return new  WaitForSeconds(0.25f);
        CanChange = true;
       // _inputDelayCoroutine = null;
    }

     IEnumerator Run()
     {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _nextPosition, speed * Time.deltaTime);
            yield return null;
        }
     }
    
    
    public void Move_Init()
    {
        StartCoroutine(Run());
            
    }
    

    public void Dead_Init()
    {
        if (!dead)
        {
            dead = true;
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
            
            Destroy(gameObject, 3);
        }

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
        if (!start)
        {
            start = true;
            StartCoroutine(Run());
        }

        
        

        if (_timerForInput<=0)
        {
            //CanChange = false;
            _timerForInput = 0.3f;
            //_inputDelayCoroutine = StartCoroutine(InputDelay());
            _changeDirection = !_changeDirection;
            mov_left = _changeDirection;
            if (!dead)
            {
                if (mov_left)
                {
                    _nextPosition = _leftNode;
                }
                else
                {
                    _nextPosition = _rightNode;

                }
            }
            
            
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
            EventManager.instance.OnJumpAction?.Invoke();
            JumpStep = 2;
            speed = 9;
            StartCoroutine(Run());
        }

        
    }
    

}