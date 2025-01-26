using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private float reduceTimer;
    
    private int _totalSccore;

    private float timer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        timer += Time.deltaTime;
        // FORMAT
        _textScore.text = timer.ToString("F2") + "s";
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
