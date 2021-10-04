using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSys : MonoBehaviour
{
    [SerializeField] GameObject[] HealthImg;

    [SerializeField] int Health = 3;

    public void DecreaseHealth(int value)
    {
        Health -= value;
        UpdateHealthUI();
        if (Health <= 0)
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    public void IncreaseHealth(int value)
    {
        Health += value;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if(Health >= 3)
        {
            HealthImg[2].SetActive(true);
            HealthImg[1].SetActive(true);
            HealthImg[0].SetActive(true);
        }
        else if(Health >= 2)
        {
            HealthImg[2].SetActive(false);
            HealthImg[1].SetActive(true);
            HealthImg[0].SetActive(true);
        }
        else if(Health >= 1)
        {
            HealthImg[2].SetActive(false);
            HealthImg[1].SetActive(false);
            HealthImg[0].SetActive(true);
        }
        else
        {
            HealthImg[2].SetActive(false);
            HealthImg[1].SetActive(false);
            HealthImg[0].SetActive(false);
        }
    }
}
