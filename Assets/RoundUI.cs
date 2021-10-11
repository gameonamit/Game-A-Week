using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundTxtOne;
    [SerializeField] private TextMeshProUGUI roundTxtTwo;

    public void UpdatePlayerRound(GameObject player, int round)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            roundTxtOne.text = round.ToString();
        }
        else if (player.gameObject.CompareTag("OtherPlayer"))
        {
            roundTxtTwo.text = round.ToString();
        }
    }
}
