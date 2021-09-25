using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject GemsPrefab;
    [SerializeField] private float spawnFrequency = 2.00f;

    [SerializeField] private Vector2 minimumPosition;
    [SerializeField] private Vector2 maximumPosition;
    [SerializeField] private float ZPosition = 10;

    private Timer timer;
    private float startingSpawnFrequency;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        startingSpawnFrequency = spawnFrequency;
    }

    private void Update()
    {
        //if (timer.isGameStarted)
        //{
        //    spawnFrequency -= Time.deltaTime;
        //    if (spawnFrequency <= 0)
        //    {
        //        spawnFrequency = startingSpawnFrequency;
        //        SpawnGems();
        //        //Spawned Gems once spawn time is less than zero
        //    }
        //}
    }

    public void SpawnGems()
    {
        Vector2 SpawnPosition;
        SpawnPosition.x = Random.Range(minimumPosition.x, maximumPosition.x);
        SpawnPosition.y = Random.Range(minimumPosition.y, maximumPosition.y);
        Instantiate(GemsPrefab, new Vector3(SpawnPosition.x, SpawnPosition.y, ZPosition), Quaternion.identity);
    }
}
