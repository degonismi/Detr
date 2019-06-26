using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed;
    public static bool mov_left,dead,start;
    public GameObject sphere,part,camera;
    float start_speed, speed_fail;

    void Start()
    {
        mov_left = true;
        dead = false;
        start = false;
        speed_fail = 0;
        sphere.GetComponent<Animator>().enabled = false;
    }
    void Update()
    {
        if (start) {
            sphere.GetComponent<Animator>().enabled = true;
            start_speed = speed;
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
    }
    public void Move_Init()
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(transform.position.x, transform.position.y - 3.5f, transform.position.z),1*Time.deltaTime) ;
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
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        speed_fail = Mathf.Lerp(speed_fail, -40, 4 * Time.deltaTime);
        transform.Translate(0, speed_fail * Time.deltaTime, 0);
        Destroy(gameObject, 1);
    }
}