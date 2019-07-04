using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance = null;

    public Action OnDeadAction;
    public Action OnVictoryAction;

    public Action OnJumpAction;
    public Action OnLevelStart;
    //public Action OnDeadAction;


    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
        }
        else if (instance == this)
        { 
            Destroy(gameObject); 
        }
    } 
}
