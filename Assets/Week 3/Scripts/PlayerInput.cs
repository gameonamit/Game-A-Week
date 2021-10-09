using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal;
    public bool jump = false;

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButtonDown("Jump");
    }
}
