using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerRounds : MonoBehaviourPun, IPunObservable
{
    public int roundsWon = 0;
    private Vector3 defaultPosition;

    private void Awake()
    {
        defaultPosition = transform.position;
    }

    public void IncreaseRound()
    {
            roundsWon += 1;
            photonView.RPC("UpdateRounds", RpcTarget.All, roundsWon);

            if (roundsWon >= 3)
            {
                string playerName = GetComponent<NetworkPlayer>().PlayerName;
                FindObjectOfType<LevelManager>().GameOver(playerName);
            }
            else
            {
                photonView.RPC("ResetPlayerPosition", RpcTarget.All);
            }
    }

    public void DecreaseRound()
    {
        roundsWon -= 1;
        photonView.RPC("UpdateRounds", RpcTarget.All, roundsWon);
    }

    [PunRPC]
    public void UpdateRounds(int rounds)
    {
        StartCoroutine(RestartRound());
        roundsWon = rounds;
        FindObjectOfType<RoundUI>().UpdatePlayerRound(this.gameObject, roundsWon);
        if (roundsWon >= 3)
        {
            string playerName = GetComponent<NetworkPlayer>().PlayerName;
            FindObjectOfType<LevelManager>().GameOver(playerName);
        }
    }

    [PunRPC]
    public void ResetPlayerPosition()
    {
        PlayerRounds []playerRounds = FindObjectsOfType<PlayerRounds>();
        foreach (PlayerRounds plaRounds in playerRounds)
        {
            plaRounds.gameObject.transform.position = plaRounds.defaultPosition;
        }
    }

    private IEnumerator RestartRound()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.PauseGame();
        levelManager.UpdateTimerText("Round Ended!");
        yield return new WaitForSeconds(3f);
        levelManager.RestartGame();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(roundsWon);
        }
        else
        {
            roundsWon = (int)stream.ReceiveNext();
        }
    }
}
