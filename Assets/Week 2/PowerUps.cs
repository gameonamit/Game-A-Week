using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float powerUpTime = 5f;
    [SerializeField] private GameObject SpiceUI;
    [SerializeField] private float increasedPlayerSize = 1.5f;

    private float alpha;
    private bool fadeIn = false;
    private bool fadeOut = false;
    private Image spiceUIImage;

    Coroutine ChilliCoroutine;
    Coroutine IncreaseSizeCoroutine;

    private void Start()
    {
        spiceUIImage = SpiceUI.GetComponent<Image>();
        alpha = spiceUIImage.color.a;
    }

    private void Update()
    {
        if (fadeIn)
        {
            if (alpha < 1)
            {
                Color col = spiceUIImage.color;
                alpha += Time.deltaTime * 1.8f;
                col.a = alpha;
                spiceUIImage.color = col;
            }
            else
            {
                fadeIn = false;
            }
        }
        if (fadeOut)
        {
            if (alpha > 0)
            {
                Color col = spiceUIImage.color;
                alpha -= Time.deltaTime * 1.8f;
                col.a = alpha;
                spiceUIImage.color = col;
            }
            else
            {
                fadeOut = false;
            }
        }
    }

    public void ActivateChilliPowerUp()
    {
        if (ChilliCoroutine != null)
        {
            StopCoroutine(ChilliCoroutine);
        }
        ChilliCoroutine = StartCoroutine(ChilliPowerUpDelay());
    }

    IEnumerator ChilliPowerUpDelay()
    {
        SpiceUI.SetActive(true);
        fadeIn = true;
        yield return new WaitForSecondsRealtime(powerUpTime);
        fadeOut = true;
        yield return new WaitForSecondsRealtime(2f);
        SpiceUI.SetActive(false);
    }

    public void IncreasePlayerSize()
    {
        if (IncreaseSizeCoroutine != null)
        {
            StopCoroutine(IncreaseSizeCoroutine);
        }
        IncreaseSizeCoroutine = StartCoroutine(IncreasePlayerSizeDelay());
    }

    IEnumerator IncreasePlayerSizeDelay()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float playerScale = player.transform.localScale.x;
        Vector3 newScale = new Vector3(increasedPlayerSize, increasedPlayerSize, increasedPlayerSize);
        player.transform.localScale = newScale;
        yield return new WaitForSecondsRealtime(powerUpTime);
        player.transform.localScale = new Vector3(playerScale, playerScale, playerScale);
    }
}
