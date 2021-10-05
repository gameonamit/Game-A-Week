using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSys : MonoBehaviour
{
    [SerializeField] GameObject[] HealthImg;

    [SerializeField] int Health = 3;

    [SerializeField] private Color activatedColor;
    [SerializeField] private Color deactivatedColor;

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
            ActivateImage(HealthImg[2]);
            ActivateImage(HealthImg[1]);
            ActivateImage(HealthImg[0]);
        }
        else if(Health >= 2)
        {
            DeactivateImage(HealthImg[2]);
            ActivateImage(HealthImg[1]);
            ActivateImage(HealthImg[0]);
        }
        else if(Health >= 1)
        {
            DeactivateImage(HealthImg[2]);
            DeactivateImage(HealthImg[1]);
            ActivateImage(HealthImg[0]);
        }
        else
        {
            DeactivateImage(HealthImg[2]);
            DeactivateImage(HealthImg[1]);
            DeactivateImage(HealthImg[0]);
        }
    }

    private void DeactivateImage(GameObject image)
    {
        image.GetComponent<Image>().color = deactivatedColor;
    }

    private void ActivateImage(GameObject image)
    {
        image.GetComponent<Image>().color = activatedColor;
    }
}
