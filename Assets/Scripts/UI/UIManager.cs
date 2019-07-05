using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gamePlayPanel;
    [SerializeField] private GameObject _deadPanel;
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _levelSelectionPanel;

    private Moving _moving;


    private void Start()
    {
        EventManager.instance.OnDeadAction += Dead;
        EventManager.instance.OnVictoryAction += Victory;
        _moving = FindObjectOfType<Moving>();

    }

    private void OnDestroy()
    {
        EventManager.instance.OnDeadAction -= Dead;
        EventManager.instance.OnVictoryAction -= Victory;
    }

    private void Dead()
    {
        _gamePlayPanel.SetActive(false);
        _deadPanel.SetActive(true);
    }

    private void Victory()
    {
        _gamePlayPanel.SetActive(false);
        _victoryPanel.SetActive(true);
    }

    public void StartGame()
    {
        _moving.StartJump();
        
    }

    public void ChangeDirection()
    {
        _moving.ChangeDirection();
    }
    
    
    
    
    
    
    
    

}
