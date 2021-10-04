using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Bomb bomb = GetComponentInParent<Bomb>();
            bomb.ColliderTriggered(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Bomb bomb = GetComponentInParent<Bomb>();
            bomb.ColliderTriggered(false);
        }
    }
}
