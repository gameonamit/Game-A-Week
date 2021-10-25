using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool fadeIn = false;
    bool playingBeatOne = false;

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
}
