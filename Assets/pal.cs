using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pal : MonoBehaviour
{
    public Animator anim;

    private void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        float magnitude = (HorizontalInput + VerticalInput);

        anim.SetFloat("", HorizontalInput);
        anim.SetFloat("", VerticalInput);
        anim.SetFloat("InputMagnitude", magnitude);
    }
}
