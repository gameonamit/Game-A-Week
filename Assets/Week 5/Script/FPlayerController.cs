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

    [SerializeField] private float m_ForwardRotationSpeed = 10f;

    private float Horizontal;

    [SerializeField] private float playerLeftEdge = -4.00f;
    [SerializeField] private float playerRightEdge = 4.00f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.velocity = Vector3.zero;
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        if (FiGameManager.instance.isStarted == true)
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
    }

    private void ApplyGravity()
    {
        Vector3 angle = new Vector3(0, 1, 0);
        transform.position += angle * m_GravityScale * Time.deltaTime;
    }

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
        Vector3 angle = new Vector3(0, 0, 1);
        transform.position += angle * m_ForwardSpeed * Time.deltaTime;
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

    public float GetCurrentHorizontal()
    {
        return Horizontal;
    }
}
