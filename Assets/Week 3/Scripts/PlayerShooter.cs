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
    [SerializeField] private ParticleSystem MuzzleFlash;

    public float bulletForce = 20f;
    public float fireRate = 10f;
    private float nextTimeToFire = 0f;
    Vector2 mousePos;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bulletSFX;

    void Update()
    {
        if (photonView.IsMine && !LevelManager.isPaused)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ApplyRotation();
            //RotateTowards();

            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
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

        audioSource.PlayOneShot(bulletSFX);
        StartCoroutine(MuzzleFlashEffect());
    }

    private IEnumerator MuzzleFlashEffect()
    {
        MuzzleFlash.Play();
        yield return new WaitForSeconds(0.15f);
        MuzzleFlash.Stop();
    }

    private void ApplyRotation()
    {
        var MousePos = Input.mousePosition;
        Vector3 firePositon = Camera.main.WorldToScreenPoint(firePoint.position);
        Vector3 lookDir = MousePos - firePositon;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        firePoint.eulerAngles = new Vector3(0, 0, angle);
    }
}
