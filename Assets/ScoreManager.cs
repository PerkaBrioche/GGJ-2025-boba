using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private float baseTimeBeforeIncrement;
    [SerializeField] private float reduceTimer;
    
    private int Increment = 1;
    private int _totalSccore;

    private float timer;
    private float timeBeforeIncrement;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        timeBeforeIncrement = baseTimeBeforeIncrement;
    }

    private void Update()
    {
        _textScore.text = _totalSccore + "m";

        if (timer < timeBeforeIncrement)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            AddScore();
            timeBeforeIncrement = baseTimeBeforeIncrement;
        }
    }


    private void AddScore()
    {
        _totalSccore += Increment;
    }

    public void NewDifficulty()
    {
        //Increment++;
        baseTimeBeforeIncrement *= reduceTimer;
    }

    private void OnValidate()
    {
        if (reduceTimer > 0.99f)
        {
            reduceTimer = 0.99f;
        }else if (reduceTimer < 0)
        {
            reduceTimer = 0.1f;
        }
    }
}
