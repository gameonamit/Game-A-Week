using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class NetworkPlayer : MonoBehaviourPun, IPunObservable
{
    public string PlayerName;
    private Vector3 networkPosition;
    private Quaternion networkRotation;
    [SerializeField] private float smoothing = 20.0f;
    [SerializeField] private TextMeshProUGUI playerNameTxt;

    private void Awake()
    {
        //destroy the controller if the player is not controlled by me
        //if (!photonView.IsMine && GetComponent<PlayerMovement>() != null)
        //    Destroy(GetComponent<PlayerMovement>());
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<PhotonView>().IsMine)
        {
            tag = "OtherPlayer";
            playerNameTxt.gameObject.SetActive(true);
            StartCoroutine(UpdatePlayerText());
        }
        else
        {
            playerNameTxt.gameObject.SetActive(true);
            StartCoroutine(MainPlayer());
        }

        networkPosition = transform.position;
        networkRotation = transform.rotation;
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
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
            PlayerName = (string)stream.ReceiveNext();
        }
    }

    public IEnumerator UpdatePlayerText()
    {
        yield return new WaitForSeconds(0.05f);
        playerNameTxt.text = PlayerName;
    }

    public IEnumerator MainPlayer()
    {
        yield return new WaitForSeconds(0.05f);
        playerNameTxt.text = "Me";
    }
}
