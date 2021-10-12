using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    [SerializeField] private CinemachineVirtualCamera cineCam;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform []SpawnPoints;

    private void Start()
    {        
        StartCoroutine(SpawnPlayer());
    }

    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject []players = GameObject.FindGameObjectsWithTag("OtherPlayer");
        if(players.Length <= 0)
        {
            Vector3 SpawnPosition = SpawnPoints[0].transform.position;
            var player = PhotonNetwork.Instantiate(playerPrefab.name, SpawnPosition, Quaternion.identity);
            string playerName = PlayerInformation.instance.PlayerName;
            player.GetComponent<NetworkPlayer>().PlayerName = playerName;
            cineCam.transform.position = SpawnPoints[0].transform.position;
        }
        else
        {
            Vector3 SpawnPosition = SpawnPoints[1].transform.position;
            var player = PhotonNetwork.Instantiate(playerPrefab.name, SpawnPosition, Quaternion.identity);
            string playerName = PlayerInformation.instance.PlayerName;
            player.GetComponent<NetworkPlayer>().PlayerName = playerName;
            cineCam.transform.position = SpawnPoints[1].transform.position;
        }
        yield return new WaitForSeconds(0.01f);
        UpdateCameraTarget();
    }

    private void UpdateCameraTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerFollowTarget followTarget = player.GetComponentInChildren<PlayerFollowTarget>();
        cineCam.Follow = followTarget.transform;
    }
}
