using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    FGameManager gameManager;

    [SerializeField] int noOfLives = 4;
    [SerializeField] GameObject[] LivesUIImage;
    [SerializeField] string NextSceneName;

    bool isGamePaused = false;

    private void Start()
    {
        UpdateLivesUI();
        gameManager = FindObjectOfType<FGameManager>();
    }

    private void UpdateLivesUI()
    {
        int length = LivesUIImage.Length;
        for(int i = 0; i < length; i++)
        {
            if(i >= noOfLives)
            {
                LivesUIImage[i].SetActive(false);
            }
        }
    }

    public void DecreaseLives()
    {
        noOfLives -= 1;
        UpdateLivesUI();
    }

    public void CheckGameOver(bool inFinishLine)
    {
        if (inFinishLine == false)
        {
            if (noOfLives <= 0)
            {
                if (!isGamePaused)
                {
                    isGamePaused = true;
                    gameManager.DisableBall();

                    //GameOver
                    Debug.Log("GameOver");
                }
            }
            else
            {
                gameManager.SpawnBall();
            }
        }
        else
        {
            if (!isGamePaused)
            {
                isGamePaused = true;
                gameManager.DisableBall();

                //Game Won
                Debug.Log("Game Won");
                GameWon();
            }
        }
    }

    private void GameWon()
    {
        SceneManager.LoadScene(NextSceneName);
    }
}
