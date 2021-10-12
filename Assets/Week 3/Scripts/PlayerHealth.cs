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
        if (!LevelManager.isPaused)
        {
            CurrentHealth += value;
            if (CurrentHealth >= 100)
            {
                CurrentHealth = 100;
            }
            UpdateUI();
        }
    }

    public void DecreaseHealth(int value)
    {
        if (!LevelManager.isPaused)
        {
            CurrentHealth -= value;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                if (this.gameObject.CompareTag("Player"))
                {
                    GameObject player = GameObject.FindGameObjectWithTag("OtherPlayer");
                    player.GetComponent<PlayerRounds>().IncreaseRound();
                }
                else
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.GetComponent<PlayerRounds>().IncreaseRound();
                }
                CurrentHealth = 100;
            }
            UpdateUI();
        }
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
