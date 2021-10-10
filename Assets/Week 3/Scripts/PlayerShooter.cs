using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerShooter : MonoBehaviourPunCallbacks
{
    public PlayerMovement playerMovement;
    public PlayerInput playerInput;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    Vector2 mousePos;

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ApplyRotation();
        }
    }

    private void Shoot()
    {
        photonView.RPC("ShootBullet", RpcTarget.All, firePoint.position, firePoint.rotation, firePoint.up);

        if (Mathf.Abs(playerInput.Horizontal) == 0)
        {
            float distance = (mousePos.x - transform.position.x);
            if (distance < 0)
                playerMovement.Flip();
            else
                playerMovement.FlipBack();
        }
    }

    [PunRPC]
    private void ShootBullet(Vector3 firePos, Quaternion fireRotation, Vector3 fireDirection)
    {
        //GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, firePoint.rotation);
        GameObject bullet = Instantiate(bulletPrefab, firePos, fireRotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(fireDirection * bulletForce, ForceMode2D.Impulse);

        if (!GetComponent<PhotonView>().IsMine)
        {
            bullet.GetComponent<ThreeBullet>().isOtherBullet = true;
        }
        else
        {

        }
    }

    private void ApplyRotation()
    {
        Vector2 lookDir = mousePos - new Vector2(firePoint.transform.position.x, firePoint.transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        firePoint.eulerAngles = new Vector3(0, 0, angle);
    }
}
