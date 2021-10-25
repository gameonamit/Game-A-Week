using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int TileLength;
    public Vector3 nextSpawnPoint;
    [SerializeField] private Transform PlatformContent;

    public List<GameObject> tiles;

    private void Start()
    {
        for (int i = 0; i < TileLength; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        GameObject temp = Instantiate(platformPrefab, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(0).transform.position;
        temp.transform.parent = PlatformContent;

        if (tiles.Count > TileLength + 3)
        {
            Destroy(tiles[0].gameObject);
            tiles.Remove(tiles[0].gameObject);
        }
        tiles.Add(temp);
    }
}
