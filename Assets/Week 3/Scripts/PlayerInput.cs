using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInput : MonoBehaviourPunCallbacks
{
    public float Horizontal;
    public bool jump = false;
    public bool bulletStopInput = false;

    private void Update()
    {
        if (photonView.IsMine && !LevelManager.isPaused)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
            jump = Input.GetButtonDown("Jump");
            bulletStopInput = Input.GetButton("BulletStop");
        }
    }
}
