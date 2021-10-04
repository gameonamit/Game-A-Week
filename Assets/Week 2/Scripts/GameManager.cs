using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float Gravity = 9.8f;
    public float maxGravity = 20f;
    public float GravityIncreaseSpeed = 1f;

    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject GameOverPanel;

    private bool isGameOver = false;

    private float startingGravity;

    private void Start()
    {
        startingGravity = Gravity;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Gravity >= maxGravity)
            return;

        if (Gravity < maxGravity)
        Gravity += GravityIncreaseSpeed * 0.2f * Time.deltaTime;
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            GamePanel.SetActive(false);
            GameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            StopAllCoroutines();
        }
    }

    public void MenuBtnClick()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex);
    }
}
