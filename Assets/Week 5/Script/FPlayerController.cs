using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPlayerController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private Transform BallModel;

    [SerializeField] private float m_GravityScale = -9.8f;

    [SerializeField] private float m_SideMovementSpeed = 40f;
    [SerializeField] private float m_SideRotationSpeed = 15f;

    [SerializeField] private float m_ForwardSpeed = 20f;
    [SerializeField] private float m_MaxForwardSpeed = 40f;
    [SerializeField] private float m_ForwardSpeedMultiplier = 1.5f;

    public float currentSpeed;

    [SerializeField] private float m_ForwardRotationSpeed = 10f;

    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float jumpDuration = 0.5f;

    private float Horizontal;
    public bool isJumping = false;

    [SerializeField] private float playerLeftEdge = -4.00f;
    [SerializeField] private float playerRightEdge = 4.00f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.velocity = Vector3.zero;
        currentSpeed = m_ForwardSpeed;
    }

    private void Update()
    {
        GetInput();

        if (transform.position.y < -10 && FiGameManager.instance.isGameOver == false)
        {
            //If ball falls off platform && game is not over -- GAME OVER
            FiGameManager.instance.GameOver();
        }
    }

    private void FixedUpdate()
    {
        if (isJumping == true)
        {
            ApplyJump();
        }
        else
        {
            ApplyGravity();
        }
        if (FiGameManager.instance.isStarted == true && FiGameManager.instance.isGameOver == false)
        {
            ApplyStrafeMovement();
            ApplyForwardMovement();
            ApplyForwardRotation();
            ApplySideRotation();
        }
    }

    private void GetInput()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Jump();
        }
    }

    private void ApplyGravity()
    {
        Vector3 angle = new Vector3(0, 1, 0);
        transform.position += angle * m_GravityScale * Time.deltaTime;
    }

    #region Jump
    private void Jump()
    {
        if(isJumping == false)
        {
            isJumping = true;
            StartCoroutine(Co_Jump());
        }
    }

    IEnumerator Co_Jump()
    {
        yield return new WaitForSeconds(jumpDuration);
        isJumping = false;
    }

    private void ApplyJump()
    {
        Vector3 angle = new Vector3(0, 1, 0);
        transform.position += angle * jumpForce * Time.deltaTime;
    }
    #endregion

    #region Movement & Rotation
    private void ApplyStrafeMovement()
    {
        Vector3 angle = new Vector3(1, 0, 0);
        Vector3 playerPos;
        playerPos = angle * Horizontal * m_SideMovementSpeed * Time.deltaTime;
        playerPos.x = Mathf.Clamp(playerPos.x, playerLeftEdge, playerRightEdge);
        transform.position += playerPos;
    }

    private void ApplyForwardMovement()
    {
        currentSpeed += m_ForwardSpeedMultiplier;
        if (currentSpeed >= m_MaxForwardSpeed) { currentSpeed = m_MaxForwardSpeed; }
        Vector3 angle = new Vector3(0, 0, 1);
        transform.position += angle * currentSpeed * Time.deltaTime;
    }

    private void ApplyForwardRotation()
    {
        Vector3 angle = new Vector3(1, 0, 0);
        BallModel.transform.Rotate(angle * m_ForwardRotationSpeed * Time.deltaTime, Space.World);
    }

    private void ApplySideRotation()
    {
        Vector3 angle = new Vector3(0, 0, -1);
        BallModel.transform.Rotate(angle * Horizontal * m_SideRotationSpeed * Time.deltaTime, Space.World);
    }
    #endregion

    public float GetCurrentHorizontal()
    {
        return Horizontal;
    }
}
