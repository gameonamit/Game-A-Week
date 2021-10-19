using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private CircleCollider2D col;
    private LevelController levelController;

    public Vector2 pos { get { return transform.position; } }

    public bool isInFinishPoint = false;
    public bool isForced = false;

    bool fading = false;
    [SerializeField] float fadingSpeed = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        levelController = FindObjectOfType<LevelController>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude <= 0.2f && isForced)
        {
            fading = true;
        }

        if (fading && !isInFinishPoint)
        {
            Color col = sr.color;
            col.a -= Time.deltaTime * fadingSpeed;
            sr.color = col;

            if(col.a <= 0)
            {
                StartCoroutine(DestoryDelay());
            }
        }
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
        isForced = true;
    }

    public void ActivateRB()
    {
        rb.isKinematic = false;
    }

    public void DeactivateRB()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }

    IEnumerator DestoryDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FinishPoint"))
        {
            isInFinishPoint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FinishPoint"))
        {
            isInFinishPoint = false;
        }
    }
}
