using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    Rigidbody2D rb;

    public PlayerInput playerInput;
    public GameObject PlayerUICanvas;
    public float movementSpeed = 15f;
    public float maxMovementSpeed = 15f;
    public float linearDrag = 10f;
    public float jumpForce = 5f;
    public float gravity = 9.8f;
    public float gravityMultiplier = 2f;
    public Transform feetPos;
    public float checkRadius = 0.5f;
    public LayerMask whatIsGround;

    private bool flipped = false;
    public bool isGrounded = false;

    private bool changingDirection => (rb.velocity.x > 0f && playerInput.Horizontal < 0f) || (rb.velocity.x < 0f && playerInput.Horizontal > 0f);

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSFX;

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
        if (photonView.IsMine)
        {
            ApplyGravity();
            //Gravity
        }

        if (photonView.IsMine && !LevelManager.isPaused)
        {
            ApplyMovement();
            //Movement

            ApplyLinearDrag();
            //Linear Drag

            //Flip
            if (playerInput.Horizontal < 0)
                Flip();
            if (playerInput.Horizontal > 0)
                FlipBack();
            //Flip
        }
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
            rb.AddForce(Vector3.right * movementSpeed * playerInput.Horizontal);
            //transform.position += Vector3.right * movementSpeed
            //    * playerInput.Horizontal * Time.deltaTime;
        }

        if (Mathf.Abs(rb.velocity.x) > maxMovementSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMovementSpeed, rb.velocity.y);
    }

    private void ApplyLinearDrag()
    {
        //Linear Drag
        if ((Mathf.Abs(playerInput.Horizontal) < 0.4f || changingDirection) && isGrounded)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    public void Flip()
    {
        if (flipped == false)
        {
            Vector3 flipScale = new Vector3(-1, 1, 1);
            transform.localScale = flipScale;
            PlayerUICanvas.transform.localScale = flipScale;
            flipped = true;
        }
    }

    public void FlipBack()
    {
        if (flipped == true)
        {
            Vector3 defaultScale = new Vector3(1, 1, 1);
            transform.localScale = defaultScale;
            PlayerUICanvas.transform.localScale = defaultScale;
            flipped = false;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            audioSource.PlayOneShot(jumpSFX);
        }
    }

    public void BulletStoped()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        FindObjectOfType<PostProcessingUpdater>().UpdateToBulletStopPost();
    }

    public void BulletStopClosed()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        FindObjectOfType<PostProcessingUpdater>().UpdateToDefaultPost();
    }
}
