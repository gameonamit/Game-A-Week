using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerController : MonoBehaviour
{
    [SerializeField] private Transform light;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float movementXOffset = 0.5f;
    private bool moving = false;

    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;

    Vector3 position;
    Vector3 distance;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.isGameStarted)
        {
            if (Input.GetMouseButtonUp(0))
            {
                moving = true;
                Vector3 mousePosition = Input.mousePosition;

                position = Camera.main.ScreenToWorldPoint(mousePosition);
                distance = (position - transform.position);
                if (distance.x < 0)
                {
                    distance -= new Vector3(movementXOffset, 0f, 0f);
                }
                else
                {
                    distance += new Vector3(movementXOffset, 0f, 0f);
                }
            }

            if (moving)
            {
                if (distance.x <= 0)
                {
                    transform.position -= speed * new Vector3(1, 0, 0) * Time.deltaTime;
                    distance += speed * new Vector3(1, 0, 0) * Time.deltaTime;
                    RotateLight(1);
                }
                else
                {
                    transform.position += speed * new Vector3(1, 0, 0) * Time.deltaTime;
                    distance -= speed * new Vector3(1, 0, 0) * Time.deltaTime;
                    RotateLight(0);
                }
                if (Mathf.Round(distance.x) == 0.00f)
                {
                    moving = false;
                    distance = Vector3.zero;
                    position = Vector3.zero;
                    return;
                }
            }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXPos, maxXPos),
                transform.position.y,
                transform.position.z);
            //Clamping to X position to min and max positon
        }
    }

    private void RotateLight(int v)
    {
        if (v == 0)
        {
            float speed = 20;
            light.Rotate(new Vector3(0, 1, 0), speed * Time.deltaTime);
        }
        else
        {
            float speed = 20;
            light.Rotate(new Vector3(0, 1, 0), -speed * Time.deltaTime);
        }
    }
}
