using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ProgressSystem : MonoBehaviour
{
    public static ProgressSystem instance;
    [SerializeField] private TextMeshProUGUI progressTxt;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateProgressText()
    {
        progressTxt.text = GetCurrentProgress().ToString() + "%";
        if(GetCurrentProgress() >= 100)
        {
            //When Progress is more than 100% -- GAME WON
            FiGameManager.instance.GameWon();
        }
    }

    public int GetCurrentProgress()
    {
        BeatGenerator beatGenerator = BeatGenerator.instance;
        return Mathf.RoundToInt(beatGenerator.activatedBeatCount / beatGenerator.beatCount * 100);
    }
}
