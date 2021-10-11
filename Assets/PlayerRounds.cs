using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerRounds : MonoBehaviourPun, IPunObservable
{
    public int roundsWon = 0;

    public void IncreaseRound()
    {
        roundsWon += 1;
        FindObjectOfType<RoundUI>().UpdatePlayerRound(this.gameObject, roundsWon);
        if(roundsWon >= 3)
        {
            FindObjectOfType<LevelManager>().GameOver();
        }
    }

    public void DecreaseRound()
    {
        roundsWon -= 1;
        FindObjectOfType<RoundUI>().UpdatePlayerRound(this.gameObject, roundsWon);
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
