using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwoScoringSys : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI gameOverScoreTxt;
    private float Score = 0f;

    public void DecreaseScore(float value)
    {
        Score -= value;
        UpdateScoreTxt();
    }

    public void IncreaseScore(float value)
    {
        Score += value;
        UpdateScoreTxt();
    }

    private void UpdateScoreTxt()
    {
        scoreTxt.text = Score.ToString();
    }

    public void UpdateGameOverTxt()
    {
        gameOverScoreTxt.text = Score.ToString();
    }
}
