using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInput : MonoBehaviourPunCallbacks
{
    public float Horizontal;
    public bool jump = false;

    private void Update()
    {
        if (photonView.IsMine)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
            jump = Input.GetButtonDown("Jump");
        }
    }
}
