using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createInput;

    public void CreateNewRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public override void OnJoinedRoom()
    {
        //SceneManager.LoadScene("Naming");
        PhotonNetwork.LoadLevel("GameScene");
    }
}
