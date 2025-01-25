using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoueController : MonoBehaviour
{
    [SerializeField] private float timeBeforeChangePosition;
    [SerializeField] private float movingDuration;
    private bool _isTimerStarted;

    [SerializeField] private Transform MinPos;
    [SerializeField] private Transform MaxPos;


    private int difficultyCounter;
     private float actualTimer;

     private void Start()
     {
         StartTimer();
     }

     private void Update()
    {
        if (_isTimerStarted)
        {
            if (actualTimer < timeBeforeChangePosition)
            {
                actualTimer += Time.deltaTime;
            }
            else
            {
                actualTimer = 0;
                ChangePositon();
                _isTimerStarted = false;
                // FINISH TIMER
            }
        }
    }

    private void ChangePositon()
    {
        Vector3 RandomPosition = new Vector3(Random.Range(MinPos.position.x, MaxPos.position.x),Random.Range(MinPos.position.y, MaxPos.position.y), Random.Range(MinPos.position.z, MaxPos.position.z));
        StartCoroutine(LerpPosition(RandomPosition));
    }

    private IEnumerator LerpPosition(Vector3 destination)
    {
        float alpha = 0;
        Vector3 myPos = transform.position;
        while (alpha < 1)
        {
            alpha += Time.deltaTime / movingDuration;
            transform.position = Vector3.Lerp(myPos, destination, alpha);
            yield return null;
        }
        transform.position = destination;

        PlatformEndTranslation();
    }

    private void PlatformEndTranslation()
    {
        difficultyCounter++;
        if (difficultyCounter > 2)
        {
            difficultyCounter = 0;
            SetNewParameters();
            ScoreManager.instance.NewDifficulty();
        }
        StartTimer();

    }
    public void StartTimer()
    {
        _isTimerStarted = true;
    }

    public void SetNewParameters()
    {
        movingDuration *= 0.95f;
        timeBeforeChangePosition *= 0.95f;
    }
}
