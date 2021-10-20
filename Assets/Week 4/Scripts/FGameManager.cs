using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FGameManager : MonoBehaviour
{
    Camera cam;
    Ball ball;
    Rigidbody2D ballRB;
    public Trajectory trajectory;
    [SerializeField] float pushForce = 5f;

    [SerializeField] Transform ballSpawnPosition;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float ballSpawnDelay = 3f;

    bool isDragging = false;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    float distance;
    Vector2 force;
    LevelController levelController;

    private void Start()
    {
        cam = Camera.main;
        SpawnBall();
        levelController = FindObjectOfType<LevelController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            OnDragStart();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }

        if (isDragging)
        {
            OnDrag();
        }

        if (ball!=null)
        {
            if (ballRB.velocity.magnitude <= 0.2f && ball.isForced)
            {
                levelController.CheckGameOver(ball.isInFinishPoint);
            }
        }
    }

    void OnDragStart()
    {
        if (ball == null) return;
        if(ball.isForced) return;
        ball.DeactivateRB();
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        trajectory.Show();
    }

    void OnDrag()
    {
        if (ball == null) return;
        if (ball.isForced) return;
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        Debug.DrawLine(startPoint, endPoint);

        //Trajectory
        trajectory.UpdateDots(ball.pos, force);
    }

    void OnDragEnd()
    {
        if (ball == null) return;
        if (ball.isForced) return;
        ball.ActivateRB();
        ball.Push(force);

        trajectory.Hide();
        levelController.DecreaseLives();
    }

    private IEnumerator SpawnNewBall()
    {
        yield return new WaitForSeconds(ballSpawnDelay);
        SpawnBall();
    }

    public void DisableBall()
    {
        ball = null;
    }

    public void SpawnBall()
    {
        GameObject ballObj = Instantiate(ballPrefab, ballSpawnPosition.position, Quaternion.identity);
        ball = ballObj.GetComponent<Ball>();
        ballRB = ballObj.GetComponent<Rigidbody2D>();

        ball.DeactivateRB();
    }
}
