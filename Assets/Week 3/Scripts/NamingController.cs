using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;

public class NamingController : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField nameInputField;

    public void OnSubmitBtnClick()
    {
        if(nameInputField.text != string.Empty)
        {
            PlayerInformation.instance.PlayerName = nameInputField.text;
            StartCoroutine(LoadGameScene());
        }
        else
        {
            Debug.Log("Name is empty");
        }
    }

    IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(0.02f);
        SceneManager.LoadScene("Selection");
    }
}
