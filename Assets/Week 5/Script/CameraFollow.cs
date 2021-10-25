using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offSet;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;

    private void FixedUpdate()
    {
        HandleTranslation();
    }

    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offSet);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }
}
