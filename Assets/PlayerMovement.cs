using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    public float movementSpeed = 15f;
    public float jumpForce = 5f;
    public float gravity = 9.8f;
    public float gravityMultiplier = 2f;

    Rigidbody rb;
    private bool flipped = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        //Gravity

        ApplyMovement();
        //Movement

        //Flip
        if(playerInput.Horizontal < 0)
        Flip();
        if (playerInput.Horizontal > 0)
        FlipBack();
        //Flip

        if (playerInput.jump)
        Jump();
        //Jump
    }

    private void ApplyGravity()
    {
        rb.AddForce(Vector3.down * gravity * gravityMultiplier);
    }

    private void ApplyMovement()
    {
        rb.AddForce(Vector3.right * movementSpeed
            * 50 * playerInput.Horizontal * Time.deltaTime);
    }

    private void Flip()
    {
        if(flipped == false)
        {
            Vector3 flipScale = new Vector3(-1, 1, 1);
            transform.localScale = flipScale;
            flipped = true;
        }
    }

    private void FlipBack()
    {
        if(flipped == true)
        {
            Vector3 defaultScale = new Vector3(1, 1, 1);
            transform.localScale = defaultScale;
            flipped = false;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
