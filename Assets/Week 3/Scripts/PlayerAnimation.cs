using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimation : MonoBehaviourPunCallbacks
{
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;

    public float runSpeed = 1.5f;

    private bool isIdle = false;
    private Animator anim;
    private Rigidbody2D rb;

    private bool grounded = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (photonView.IsMine)
        {
            anim.SetFloat("XVelocity", Mathf.Abs(rb.velocity.x));
            if (Mathf.Abs(playerInput.Horizontal) <= 0)
            {
                anim.SetFloat("XVelocity", 0f);
            }

            if (playerMovement.isGrounded && !grounded)
            {
                grounded = true;
                anim.SetBool("isGrounded", playerMovement.isGrounded);
            }
            else if (!playerMovement.isGrounded && grounded)
            {
                grounded = false;
                anim.SetBool("isGrounded", playerMovement.isGrounded);
            }
        }
    }
}
