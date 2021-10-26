using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FiGameManager : MonoBehaviour
{
    public static FiGameManager instance;
    AudioSource audioSource;

    public bool isStarted = false;

    [SerializeField] AudioClip musicClip;
    [SerializeField] float startOffset = 3.5f;
    [SerializeField] float volume = 0.5f;

    [SerializeField] AudioSource backAudioSource;
    [SerializeField] AudioClip beatOne;
    [SerializeField] AudioClip beatTwo;

    [SerializeField] GameObject GUI;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject GameWonMenu;

    [SerializeField] TextMeshProUGUI GameOverScoreTxt;
    [SerializeField] TextMeshProUGUI GameWonScoreTxt;

    [SerializeField] TextMeshProUGUI GameOverProgressTxt;
    [SerializeField] TextMeshProUGUI GameWonProgressTxt;

    [SerializeField] BeatGenerator beatGen;
    [SerializeField] FPlayerController player;

    bool fadeIn = false;
    bool playingBeatOne = false;
    public bool isGameOver = false;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(isStarted == false)
            {
                isStarted = true;
                audioSource.clip = musicClip;
                audioSource.time = startOffset;
                audioSource.Play();
                fadeIn = true;
            }
        }

        if (fadeIn == true)
        {
            if(audioSource.volume < volume)
            {
                audioSource.volume += Time.deltaTime * 0.2f;
            }
        }
    }

    public void PlaySongBeat()
    {
        if(playingBeatOne == false)
        {
            backAudioSource.PlayOneShot(beatOne);
            playingBeatOne = true;
        }
        else
        {
            backAudioSource.PlayOneShot(beatTwo);
            playingBeatOne = false;
        }
    }

    #region GAME OVER
    public void GameOver()
    {
        isGameOver = true;
        GameOverMenu.SetActive(true);
        GUI.SetActive(false);
        UpdateGameOverMenuUI();
        beatGen.DisableAllBeat();
        StopMusic();
    }

    private void UpdateGameOverMenuUI()
    {
        GameOverScoreTxt.text = FScoringSystem.instance.score.ToString();
        GameOverProgressTxt.text = ProgressSystem.instance.GetCurrentProgress().ToString() + "%";
    }
    #endregion

    #region GAME WON
    public void GameWon()
    {
        isGameOver = true;
        GameWonMenu.SetActive(true);
        GUI.SetActive(false);
        UpdateGameWonMenuUI();
        beatGen.DisableAllBeat();
        StopMusic();
    }

    private void UpdateGameWonMenuUI()
    {
        GameWonScoreTxt.text = FScoringSystem.instance.score.ToString();
        GameWonProgressTxt.text = ProgressSystem.instance.GetCurrentProgress().ToString() + "%";
    }
    #endregion

    private void StopMusic()
    {
        audioSource.Stop();
        // Maybe play music stoped sound
    }

    public Transform GetPlayer()
    {
        return player.transform;
    }
}
