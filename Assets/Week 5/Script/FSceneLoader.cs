using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FSceneLoader : MonoBehaviour
{
    public void OnMenuBtnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void OnExitBtnClick()
    {
        Application.Quit();
    }
}
