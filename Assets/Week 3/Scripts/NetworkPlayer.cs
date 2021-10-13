using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class NetworkPlayer : MonoBehaviourPun, IPunObservable
{
    public string PlayerName;
    public string PlayerColorHex;
    private Vector3 networkPosition;
    private Quaternion networkRotation;
    [SerializeField] private float smoothing = 20.0f;
    [SerializeField] private TextMeshProUGUI playerNameTxt;
    [SerializeField] private SpriteRenderer spriteRen;

    private void Awake()
    {
        if (!GetComponent<PhotonView>().IsMine)
        {
            tag = "OtherPlayer";
            playerNameTxt.gameObject.SetActive(true);
            StartCoroutine(UpdatePlayerText());
            AudioListener audioListener = GetComponent<AudioListener>();
            Destroy(audioListener);
        }
        else
        {
            playerNameTxt.gameObject.SetActive(true);
            StartCoroutine(MainPlayer());
        }

        networkPosition = transform.position;
        networkRotation = transform.rotation;
        FindObjectOfType<LevelManager>().IncreasePlayerSpawned();
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * smoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * smoothing);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(PlayerName);
            stream.SendNext(PlayerColorHex);
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
            PlayerName = (string)stream.ReceiveNext();
            PlayerColorHex = (string)stream.ReceiveNext();
        }
    }

    public IEnumerator UpdatePlayerText()
    {
        yield return new WaitForSeconds(0.2f);
        playerNameTxt.text = PlayerName;
        Color col;
        ColorUtility.TryParseHtmlString(PlayerColorHex, out col);
        FindObjectOfType<PlayerNameUI>().UpdatePlayerName(this.gameObject, PlayerName);
        spriteRen.color = col;
    }

    public IEnumerator MainPlayer()
    {
        yield return new WaitForSeconds(0.2f);
        playerNameTxt.text = "Me";
        Color col;
        ColorUtility.TryParseHtmlString(PlayerColorHex, out col);
        FindObjectOfType<PlayerNameUI>().UpdatePlayerName(this.gameObject, PlayerName);
        spriteRen.color = col;
    }
}
