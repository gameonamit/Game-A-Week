using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 20f;

    private void Update()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, target.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
