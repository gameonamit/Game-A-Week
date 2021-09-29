using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody rb;
    public float speed = 5f;

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
        Movement();
        //Get Input and Apply movement to player
    }

    private void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRecording = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRecording = false;
        }

        if (isRecording)
        {
            float newX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            float newY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            rb.AddForce(new Vector3(newX * speed * Time.deltaTime,
                newY * speed * Time.deltaTime, 0f));
        }
    }

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
