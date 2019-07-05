using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class LevelManager : MonoBehaviour
{
   public LevelsSO LevelsSO;
   private int _levelNumber;

   public GameObject GetLevelPrefab(int i)
   {
      GameObject level = LevelsSO.Levels[i].LevelPrefab;
      return level;
   }

   private void Start()
   {
      _levelNumber = UnityEngine.Random.Range(0, 4);
      Instantiate(GetLevelPrefab(_levelNumber));
   }
}
