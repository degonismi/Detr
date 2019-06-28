using System.Collections;
using System.Collections.Generic;
using Imphenzia;
using UnityEngine;

public class BackGroundsGradients : MonoBehaviour
{
    [SerializeField] private Gradient[] _gradients;

    private void Start()
    {
        SetRandomGradient();
    }

    private void SetRandomGradient()
    {

        int i = Random.Range(0, _gradients.Length);
        while (i==PlayerPrefs.GetInt("PrevGradient",0))
        {
            i = Random.Range(0, _gradients.Length);
        }
        PlayerPrefs.SetInt("PrevGradient",i);
        GetComponent<GradientSkyObject>().gradient = _gradients[i];
    }
}
