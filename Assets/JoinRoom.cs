using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class JoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField joinInput;

    public void JoinNewRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }
}
