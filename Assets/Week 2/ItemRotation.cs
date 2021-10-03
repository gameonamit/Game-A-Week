using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    private int RotateType;

    private void Start()
    {
        RotateType = Random.Range(0, 4); ;
    }

    void Update()
    {
        if (RotateType == 0)
        {
            transform.Rotate(new Vector3(1, 0, 1) * rotationSpeed * Time.deltaTime, Space.Self);
        }
        else if(RotateType == 1)
        {
            transform.Rotate(new Vector3(-1, 0, -1) * rotationSpeed * Time.deltaTime, Space.Self);
        }
        else if (RotateType == 2)
        {
            transform.Rotate(new Vector3(1, 0, -1) * rotationSpeed * Time.deltaTime, Space.Self);
        }
        else if (RotateType == 3)
        {
            transform.Rotate(new Vector3(-1, 0, 1) * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}
