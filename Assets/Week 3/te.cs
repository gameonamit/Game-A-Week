using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class te : MonoBehaviour
{
    public GameObject theEnemy;
    public float minXPos;
    public float maxXPos;
    public float minZPos;
    public float maxZPos;
    public int enemyCount;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        int newEnemyCount = 0;
        while (newEnemyCount < enemyCount)
        {
            float xPos = Random.Range(minXPos, maxXPos);
            float zPos = Random.Range(minZPos, maxZPos);
            Instantiate(theEnemy, new Vector3(xPos, -0.015f, zPos), Quaternion.identity);
            newEnemyCount++;
        }
        yield return new WaitForSeconds(0.2f);
    }
}
