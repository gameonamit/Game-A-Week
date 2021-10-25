using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    PlatformSpawner platformSpawner;
    [SerializeField] float destoryTime = 5f;

    private void Awake()
    {
        platformSpawner = FindObjectOfType<PlatformSpawner>();
    }

    private void Start()
    {
        StartCoroutine(Co_DestoryAfterTime());
    }

    private IEnumerator Co_DestoryAfterTime()
    {
        yield return new WaitForSeconds(destoryTime);
        //Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            platformSpawner.SpawnTile();
        }
    }
}
