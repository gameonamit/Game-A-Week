using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FScoringSystem : MonoBehaviour
{
    public static FScoringSystem instance;

    [SerializeField] private TextMeshProUGUI scoreTxt;
    public int score = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreTxt.text = score.ToString();
    }

    public void AddScore(int sco)
    {
        score += sco;
        UpdateScoreText();
    }
}
