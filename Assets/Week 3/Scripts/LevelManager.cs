using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviourPunCallbacks
{
    private int NoOfRounds  = 3;
    public static bool isPaused = true;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TextMeshProUGUI timerTxt;
    private Animator timerAnim;
    [SerializeField] private TextMeshProUGUI resultsTxt;
    private int playersSpawned = 0;

    private void Awake()
    {
        timerAnim = timerTxt.GetComponent<Animator>();
        timerTxt.gameObject.SetActive(true);
        timerAnim.SetTrigger("Zoom");
        timerTxt.text = "Waiting for other player.";
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        isPaused = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
    }

    public void GameOver(string text)
    {
        PauseGame();
        gameOverMenu.SetActive(true);
        resultsTxt.text = text + " won the Game.";
    }

    public void OnMenuBtnClick()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Loading");
    }

    public void IncreasePlayerSpawned()
    {
        playersSpawned += 1;
        if (playersSpawned >= 2)
        {
            StartCoroutine(StartTheGame());
        }
    }

    public void RestartGame()
    {
        StartCoroutine(StartTheGame());
    }

    IEnumerator StartTheGame()
    {
        isPaused = true;
        timerAnim.SetTrigger("Zoom");
        timerTxt.text = "3";
        yield return new WaitForSeconds(1f);
        timerAnim.SetTrigger("Zoom");
        timerTxt.text = "2";
        yield return new WaitForSeconds(1f);
        timerAnim.SetTrigger("Zoom");
        timerTxt.text = "1";
        yield return new WaitForSeconds(1f);
        timerAnim.SetTrigger("Zoom");
        timerTxt.text = "Go!";
        isPaused = false;
        yield return new WaitForSeconds(1f);
        timerTxt.text = "";
    }

    public void UpdateTimerText(string txt)
    {
        timerAnim.SetTrigger("Zoom");
        timerTxt.text = txt;
    }
}
