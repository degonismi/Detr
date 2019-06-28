using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject retry_button,start_button;
    public static bool retry_active;

    private Moving _moving;

    void Start()
    {
        _moving = FindObjectOfType<Moving>();
        retry_active = false;
        retry_button.SetActive(false);
    }
    private void Update()
    {
        if (retry_active)
        {
            retry_UI();
        }
    }
    public void retry_UI()
    {
        retry_button.SetActive(true);
    }
    public void Retry()
    {
        Application.LoadLevel(0);
    }
    public void Starting()
    {
        _moving.start = true;
        start_button.SetActive(false);
    }
}
