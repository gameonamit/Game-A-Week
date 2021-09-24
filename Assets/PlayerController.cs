using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;

    private Vector2 mousePosition;
    private float deltaX, deltaY;

    [SerializeField] private float minimumScale, MaximumScale;
    [SerializeField] private float scaleFactor = 0.2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.AddForce(new Vector3((mousePosition.x - deltaX) * speed * Time.deltaTime,
            (mousePosition.y - deltaY) * speed * Time.deltaTime, 0f));
    }

    public void IncreaseScale()
    {
        transform.localScale += new Vector3(scaleFactor, scaleFactor, scaleFactor);
        if (transform.localScale.x >= MaximumScale)
        {
            transform.localScale = new Vector3(MaximumScale, MaximumScale, MaximumScale);
        }
    }
}
