using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Awake()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX),
            Random.Range(minY, maxY));
        var player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        string playerName = PlayerInformation.instance.PlayerName;
        player.GetComponent<NetworkPlayer>().PlayerName = playerName;
    }
}
