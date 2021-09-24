using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float maximumTime = 10f;

    private void Update()
    {
        if (maximumTime > 0)
        {
            maximumTime -= Time.deltaTime;
            timerText.text = maximumTime.ToString("F2");
        }
        else
        {
            //GameOver Here
            //GameOver();
        }
    }

    public void AddTime(float time)
    {
        maximumTime += time;
    }

    public void DecreaseTime(float time)
    {
        maximumTime -= time;
    }
}
