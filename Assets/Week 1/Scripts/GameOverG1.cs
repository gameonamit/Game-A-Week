using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverG1 : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject HowToPlayMenu;

    public void OnRestartBtnClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnPlayBtnClick()
    {
        MainMenu.SetActive(false);
        FindObjectOfType<Timer>().isGameStarted = true;
        FindObjectOfType<GemsSpawner>().SpawnGems();
    }

    public void OnHowToPlayHover()
    {
        HowToPlayMenu.SetActive(true);
    }

    public void OnHowToPlayHoverExit()
    {
        HowToPlayMenu.SetActive(false);
    }
}
