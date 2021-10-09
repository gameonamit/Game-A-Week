using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public PlayerInput playerInput;
    public float movementSpeed = 15f;
    public float glideSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = 9.8f;
    public float gravityMultiplier = 2f;
    public Transform feetPos;
    public float checkRadius = 0.5f;
    public LayerMask whatIsGround;

    private bool flipped = false;
    public bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (playerInput.jump)
            Jump();
        //Jump

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        //Ground Check
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
    }

    private void ApplyGravity()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.down * gravity);
        }
        else
        {
            rb.AddForce(Vector3.down * gravity * gravityMultiplier);
        }
    }

    private void ApplyMovement()
    {
        if (Mathf.Abs(playerInput.Horizontal) > 0)
        {
            //rb.AddForce(Vector3.right * movementSpeed
            //    * 60 * playerInput.Horizontal * Time.deltaTime);
            transform.position += Vector3.right * movementSpeed
                * playerInput.Horizontal * Time.deltaTime;

        }
        else
        {
            //Side Movement
            if (!isGrounded)
            {
                if (flipped)
                    transform.position += Vector3.left * glideSpeed * Time.deltaTime;
                else
                    transform.position += Vector3.right * glideSpeed * Time.deltaTime;
            }
        }
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
        if(isGrounded)
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }
}
