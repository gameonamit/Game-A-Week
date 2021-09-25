using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody rb;
    public float speed = 5f;

    private Vector2 mousePosition;
    private float deltaX, deltaY;

    [SerializeField] private float MinimumScale, MaximumScale;
    [SerializeField] private float scaleFactor = 0.2f;
    [SerializeField] private AudioClip []BallHitSFX;
    bool isRecording = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isRecording = true;
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRecording = false;
        }

        if (isRecording)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.AddForce(new Vector3((mousePosition.x - deltaX) * speed * Time.deltaTime,
                (mousePosition.y - deltaY) * speed * Time.deltaTime, 0f));
        }
    }

    //private void OnMouseDown()
    //{
    //    deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
    //    deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;

    //    //rb.velocity = Vector3.zero;
    //    //rb.angularVelocity = Vector3.zero;
    //}

    //private void OnMouseDrag()
    //{
    //    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    rb.AddForce(new Vector3((mousePosition.x - deltaX) * speed * Time.deltaTime,
    //        (mousePosition.y - deltaY) * speed * Time.deltaTime, 0f));
    //}

    public void IncreaseScale()
    {
        transform.localScale += new Vector3(scaleFactor, scaleFactor, scaleFactor);
        if (transform.localScale.x >= MaximumScale)
        {
            transform.localScale = new Vector3(MaximumScale, MaximumScale, MaximumScale);
        }
    }

    public void DecreaseScale()
    {
        transform.localScale -= new Vector3(scaleFactor, scaleFactor, scaleFactor);
        if (transform.localScale.x <= MinimumScale)
        {
            transform.localScale = new Vector3(MinimumScale, MinimumScale, MinimumScale);
        }
    }

    private void PlayBallHitSFX()
    {
        int ran = Random.Range(0, BallHitSFX.Length);
        audioSource.PlayOneShot(BallHitSFX[ran]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayBallHitSFX();
            //Ball hitting sound effect
        }
    }
}
