using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerHealth : MonoBehaviourPunCallbacks
{
    public int CurrentHealth = 100;
    public Slider healthSlider;

    PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            healthSlider.gameObject.SetActive(false);
        }
        else
        {
            healthSlider.gameObject.SetActive(true);
        }
    }

    public void IncreaseHealth(int value)
    {
        CurrentHealth -= value;
        if (CurrentHealth >= 100)
            CurrentHealth = 100;
        UpdateUI();
    }

    public void DecreaseHealth(int value)
    {
        CurrentHealth -= value;
        if (CurrentHealth <= 0)
            CurrentHealth = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (photonView.IsMine)
        {
            HealthUI healthUI = FindObjectOfType<HealthUI>();
            healthUI.UpdateHealthUI(CurrentHealth);
        }
        else
        {
            healthSlider.value = CurrentHealth;
        }
    }
}
