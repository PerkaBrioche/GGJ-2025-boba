using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class BestScoreScipt : MonoBehaviour
{
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI actualScore;
    int bestScoreNumber=0;

    private void Start()
    {

        if (actualScore != null)
        {
            int score = PlayerPrefs.GetInt("ActualScore");
            actualScore.text = score.ToString();
        }
        bestScoreNumber = PlayerPrefs.GetInt("BestScore");
        bestScore.text = bestScoreNumber.ToString();

        
    }
}
