using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float Gravity = 9.8f;
    public float maxGravity = 20f;
    public float GravityIncreaseSpeed = 1f;

    private float startingGravity;

    private void Start()
    {
        startingGravity = Gravity;
    }

    private void Update()
    {
        if (Gravity >= maxGravity)
            return;

        if (Gravity < maxGravity)
        Gravity += GravityIncreaseSpeed * 0.2f * Time.deltaTime;
    }
}
