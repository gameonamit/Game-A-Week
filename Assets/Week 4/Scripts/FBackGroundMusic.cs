using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBackGroundMusic : MonoBehaviour
{
    public static FBackGroundMusic instance;
    AudioSource audioSource;
    [SerializeField] AudioSource buttonAudioSource;

    private void Awake()
    {
        if(instance != null) 
        {
            Destroy(this.gameObject);
        }
        else 
        { 
            DontDestroyOnLoad(this.gameObject); 
            instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayButtonSound()
    {
        buttonAudioSource.Play();
    }
}
