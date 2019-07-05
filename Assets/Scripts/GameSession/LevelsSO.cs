using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Levels")]
public class LevelsSO : ScriptableObject
{
    public Level[] Levels;
    
    [Serializable]
    public class Level
    {
        public string Name;
        public GameObject LevelPrefab;

    }
}
