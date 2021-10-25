using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ProgressSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI progressTxt;

    BeatGenerator beatGenerator;

    private void Awake()
    {
        beatGenerator = FindObjectOfType<BeatGenerator>();
    }

    public void UpdateProgressText()
    {
        progressTxt.text = (Mathf.RoundToInt(beatGenerator.activatedBeatCount / beatGenerator.beatCount * 100)).ToString() + "%";
    }
}
