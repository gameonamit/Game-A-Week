using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingTexture : MonoBehaviour
{
    public float speed = 5f;
    RawImage rawImage;
    private float Width;
    private float Height;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
        Width = rawImage.uvRect.width;
        Height = rawImage.uvRect.height;
    }

    private void LateUpdate()
    {
        float yOff = rawImage.uvRect.y;
        yOff += speed * Time.deltaTime;
        rawImage.uvRect = new Rect(rawImage.uvRect.x, yOff, Width, Height);
    }
}
