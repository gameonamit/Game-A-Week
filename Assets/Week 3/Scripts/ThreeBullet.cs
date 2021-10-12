using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ThreeBullet : MonoBehaviourPunCallbacks
{
    public int Damage = 20;
    public float lifeTime = 6f;
    public bool isOtherBullet = false;
    public GameObject BulletImpactEffect;

    private void Start()
    {
        StartCoroutine(DelayDestroy());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isOtherBullet)
            {
                other.gameObject.GetComponent<PlayerHealth>().DecreaseHealth(Damage);
                Destroy(this.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("OtherPlayer"))
        {
            if (isOtherBullet)
            {

            }
            else
            {
                other.gameObject.GetComponent<PlayerHealth>().DecreaseHealth(Damage);
                Destroy(this.gameObject);
            }
        }
        else
        {
            Instantiate(BulletImpactEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSecondsRealtime(lifeTime);
        Destroy(this.gameObject);
    }
}
