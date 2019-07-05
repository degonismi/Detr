using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GamePlay : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button JumpButton;
    
    
    
    
    private Moving _moving;


    private void Start()
    {
        _moving = FindObjectOfType<Moving>();
    }
    
   // private void 
    
    
    
}
