using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI surviveText;
    [SerializeField] private float maximumTime = 10f;
    private float surviveTime = 0f;

    private bool isGameOver = false;
    public bool isGameStarted = false;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (isGameStarted && !isGameOver)
        {
            if (maximumTime > 0)
            {
                maximumTime -= Time.deltaTime;
                timerText.text = maximumTime.ToString("F2");
            }
            else
            {
                if (!isGameOver)
                {
                    isGameOver = true;
                    timerText.text = "0";
                    //GameOver Here
                    Invoke("GameOver", 1f);
                }
            }

            //Survive Time
            surviveTime += Time.deltaTime;
        }
    }

    public void AddTime(float time)
    {
        if (!isGameOver)
        {
            maximumTime += time;
        }
    }

    public void DecreaseTime(float time)
    {
        if (!isGameOver)
        {
            maximumTime -= time;
        }
    }

    private void GameOver()
    {
        GameOverMenu.SetActive(true);
        surviveText.text = surviveTime.ToString("F2") + " sec.";
        Time.timeScale = 0f;
    }
}
