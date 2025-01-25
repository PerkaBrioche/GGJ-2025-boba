using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    
    [SerializeField] private int Increment;
    private int _totalSccore;

    private float timer;
    private float timeBeforeIncrement;


    private void Update()
    {
        
    }

    private void AddScore()
    {
        _totalSccore += Increment;
    }

    public void NewDifficulty()
    {
        
    }
}
