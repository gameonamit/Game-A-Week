using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoBackgroundMusic : MonoBehaviour
{
    public static TwoBackgroundMusic instance;
    [SerializeField] private AudioSource audioSourceOne;
    [SerializeField] private AudioSource audioSourceTwo;

    [SerializeField] private AudioClip menuAudioClip;
    [SerializeField] private AudioClip gameAudioClip;

    [SerializeField] private float fadingSpeed = 10f;

    private bool audioOneFadeIn = false;
    private bool audioTwoFadeIn = false;
    private bool audioOneFadeOut = false;
    private bool audioTwoFadeOut = false;

    [SerializeField] private float maxVolume = 0.8f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            PlayMenuBGMusic();
        }
    }

    private void Update()
    {
        FadeInCheck();
        FadeOutCheck();
        //Checking for audioclip fade In or fade Out
    }

    private void FadeInCheck()
    {
        if (audioOneFadeIn)
        {
            if (audioSourceOne.volume > 0)
            {
                audioSourceOne.volume -= Time.deltaTime * fadingSpeed * 0.02f;
            }
            else
            {
                audioOneFadeIn = false;
            }
        }
        if (audioTwoFadeIn)
        {
            if (audioSourceTwo.volume > 0)
            {
                audioSourceTwo.volume -= Time.deltaTime * fadingSpeed * 0.02f;
            }
            else
            {
                audioTwoFadeIn = false;
            }
        }
    }

    private void FadeOutCheck()
    {
        if (audioOneFadeOut)
        {
            if (audioSourceOne.volume < maxVolume)
            {
                audioSourceOne.volume += Time.deltaTime * fadingSpeed * 0.02f;
            }
            else
            {
                audioOneFadeOut = false;
            }
        }
        if (audioTwoFadeOut)
        {
            if (audioSourceTwo.volume < maxVolume)
            {
                audioSourceTwo.volume += Time.deltaTime * fadingSpeed * 0.02f;
            }
            else
            {
                audioTwoFadeOut = false;
            }
        }
    }

    public void PlayMenuBGMusic()
    {
        audioSourceOne.clip = menuAudioClip;

        audioSourceOne.volume = 0;
        audioSourceOne.Play();
        audioOneFadeOut = true;
        audioTwoFadeIn = true;
    }

    public void PlayGameBGMusic()
    {
        audioSourceTwo.clip = gameAudioClip;

        audioSourceTwo.volume = 0;
        audioSourceTwo.Play();
        audioOneFadeIn = true;
        audioTwoFadeOut = true;
    }

    public void FadeInAudioClip(AudioClip audioClip)
    {
        if(audioSourceOne.isPlaying)
        {
            audioSourceTwo.clip = audioClip;
            audioSourceOne.volume = 0;
            audioSourceOne.Play();
            audioOneFadeOut = true;
            audioTwoFadeIn = true;
        }
        else if (audioSourceTwo.isPlaying)
        {
            audioSourceOne.clip = audioClip;
            audioSourceTwo.volume = 0;
            audioSourceTwo.Play();
            audioOneFadeIn = true;
            audioTwoFadeOut = true;
        }
        else
        {
            audioSourceOne.clip = audioClip;
            audioSourceTwo.volume = 0;
            audioSourceTwo.Play();
            audioOneFadeIn = true;
            audioTwoFadeOut = true;
        }
    }
}
