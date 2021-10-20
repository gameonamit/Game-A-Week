using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void OnStartBtnClick()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnQuitBtnClick()
    {
        Application.Quit();
    }
}
