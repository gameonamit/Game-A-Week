using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingUpdater : MonoBehaviour
{
    [SerializeField] private PostProcessProfile defaultProfile;
    [SerializeField] private PostProcessProfile bulletStopProfile;

    private PostProcessVolume volume;

    private void Awake()
    {
        volume = GetComponent<PostProcessVolume>();
    }

    public void UpdateToDefaultPost()
    {
        volume.profile = defaultProfile;
    }

    public void UpdateToBulletStopPost()
    {
        volume.profile = bulletStopProfile;
    }
}
