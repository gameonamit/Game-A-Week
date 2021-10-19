using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryDot : MonoBehaviour
{
    public bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isColliding = false;
        }
    }
}
