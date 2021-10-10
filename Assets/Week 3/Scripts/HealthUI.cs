using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using Photon.Pun;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    public void UpdateHealthUI(float value)
    {
        healthSlider.value = value;
    }
}
