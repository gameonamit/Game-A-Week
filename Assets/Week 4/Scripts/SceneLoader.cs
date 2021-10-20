using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void OnStartBtnClick()
    {
        FBackGroundMusic.instance.PlayButtonSound();
        SceneManager.LoadScene("Level1");
    }

    public void OnQuitBtnClick()
    {
        FBackGroundMusic.instance.PlayButtonSound();
        Application.Quit();
    }
}
