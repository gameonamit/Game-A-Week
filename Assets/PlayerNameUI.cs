using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameOne;
    [SerializeField] private TextMeshProUGUI playerNameTwo;

    public void UpdatePlayerName(GameObject player, string name)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            playerNameOne.text = name;
        }
        else if (player.gameObject.CompareTag("OtherPlayer"))
        {
            playerNameTwo.text = name;
        }
    }
}
