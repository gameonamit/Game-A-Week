using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour
{
    FGameManager gameManager;

    [SerializeField] int noOfLives = 4;
    [SerializeField] GameObject[] LivesUIImage;
    [SerializeField] string NextSceneName;
    [SerializeField] TextMeshProUGUI indicatorTxt;

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
                    StartCoroutine(GameOver());
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
                StartCoroutine(GameWon());
            }
        }
    }

    private IEnumerator GameWon()
    {
        indicatorTxt.gameObject.SetActive(true);
        indicatorTxt.text = "Sucesss!";
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(NextSceneName);
    }

    private IEnumerator GameOver()
    {
        indicatorTxt.gameObject.SetActive(true);
        indicatorTxt.text = "Failed!";
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
