using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class SelectionController : MonoBehaviourPunCallbacks
{
    [SerializeField] private Image playerImage;
    private bool selection = false;

    public void OnSubmitBtnClick()
    {
        if (selection == true)
        {
            StartCoroutine(LoadGameScene());
        }
        else
        {
            Debug.Log("Color not selected!");
        }
    }

    public void UpdateCurrentColor(Color col)
    {
        string colorHex = "#" + ColorUtility.ToHtmlStringRGB(col);
        PlayerInformation.instance.PlayerSkinColorHex = colorHex;
        playerImage.color = col;
        selection = true;
    }

    IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(0.02f);
        PhotonNetwork.LoadLevel("Lobby");
    }
}
