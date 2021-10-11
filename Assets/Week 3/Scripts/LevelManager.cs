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
    private int playersSpawned = 0;

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

    public void GameOver()
    {
        PauseGame();
        gameOverMenu.SetActive(true);
    }

    public void OnMenuBtnClick()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Loading");
    }

    public void IncreasePlayerSpawned()
    {
        photonView.RPC("PlayerSpawned", RpcTarget.All);
    }

    [PunRPC]
    private void PlayerSpawned()
    {
        playersSpawned += 1;
        if (playersSpawned >= 2)
        {
            StartCoroutine(StartTheGame());
        }
    }

    IEnumerator StartTheGame()
    {
        timerTxt.gameObject.SetActive(true);
        timerTxt.text = "3";
        yield return new WaitForSeconds(1f);
        timerTxt.text = "2";
        yield return new WaitForSeconds(1f);
        timerTxt.text = "1";
        yield return new WaitForSeconds(1f);
        timerTxt.text = "Go!";
        isPaused = false;
        yield return new WaitForSeconds(1f);
        timerTxt.text = "";
        timerTxt.gameObject.SetActive(false);
    }
}
