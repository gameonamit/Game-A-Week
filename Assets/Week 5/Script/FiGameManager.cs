using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField] GameObject startingText;
    [SerializeField] ParticleSystem particleEffect;
    [SerializeField] Animator camAnim;

    bool fadeIn = false;
    bool playingBeatOne = false;
    public bool isGameOver = false;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.volume = 0f;
        audioSource.enabled = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(isStarted == false)
            {
                isStarted = true;
                audioSource.enabled = true;
                audioSource.clip = musicClip;
                audioSource.time = startOffset;
                audioSource.Play();
                fadeIn = true;
                startingText.SetActive(false);
                particleEffect.Play();
            }
        }

        if (fadeIn == true && isGameOver == false)
        {
            if(audioSource.volume < volume)
            {
                audioSource.volume += Time.deltaTime * 0.2f;

                if(audioSource.volume > volume)
                {
                    audioSource.volume = volume;
                    //Clamping audioSource to max voulem
                }
            }
        }
    }

    public void PlaySongBeat()
    {
        if(playingBeatOne == false)
        {
            backAudioSource.PlayOneShot(beatOne);
            playingBeatOne = true;
            camAnim.SetTrigger("Shake");
        }
        else
        {
            backAudioSource.PlayOneShot(beatTwo);
            playingBeatOne = false;
            camAnim.SetTrigger("Shake");
        }
    }

    #region GAME OVER
    public void GameOver()
    {
        StartCoroutine(Co_GameOver());
    }

    IEnumerator Co_GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        isGameOver = true;
        GameOverMenu.SetActive(true);
        GameOverMenu.GetComponentInChildren<Button>().Select();
        GUI.SetActive(false);
        UpdateGameOverMenuUI();
        beatGen.DisableAllBeat();
        StopMusic();
        particleEffect.Stop();
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
        StartCoroutine(Co_GameWon());
    }

    IEnumerator Co_GameWon()
    {
        yield return new WaitForSeconds(2f);
        isGameOver = true;
        GameWonMenu.SetActive(true);
        GameWonMenu.GetComponentInChildren<Button>().Select();
        GUI.SetActive(false);
        UpdateGameWonMenuUI();
        beatGen.DisableAllBeat();
        particleEffect.Stop();
        yield return new WaitForSeconds(5f);
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
        StartCoroutine(Co_StopMusic());
        // Maybe play music stoped sound
    }

    IEnumerator Co_StopMusic()
    {
        while (audioSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
            float cVolume = audioSource.volume;
            audioSource.volume = cVolume - Time.deltaTime * 0.5f;
            if(audioSource.volume <= 0)
            {
                audioSource.Stop();
                yield return new WaitForEndOfFrame();
                audioSource.enabled = false;
            }
        }
        
    }

    public Transform GetPlayer()
    {
        return player.transform;
    }
}
