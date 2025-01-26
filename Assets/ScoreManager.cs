using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private float reduceTimer;
    
    public int _totalScore;

    public float timer;

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

    public void PlayerIsDead()
    {
        _totalScore = Mathf.FloorToInt(timer);
        int old = PlayerPrefs.GetInt("ActualScore");
        PlayerPrefs.SetInt("ActualScore", _totalScore);
        if(_totalScore >= old)
        {
            PlayerPrefs.SetInt("BestScore", _totalScore);
        }
    }
}
