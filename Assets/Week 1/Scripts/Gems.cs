using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip GemPopSound;
    [SerializeField] private GameObject GemPopParticleEffect;

    [SerializeField] private float AdditionTime = 2f;
    [SerializeField] private float gemLifetime = 3f;

    Coroutine DestroyCR;
    private bool isCollided = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DestroyCR = StartCoroutine(DestoryAfterLifetime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isCollided)
            {
                isCollided = true;
                Timer timer = FindObjectOfType<Timer>();
                timer.AddTime(AdditionTime);
                other.GetComponent<PlayerController>().DecreaseScale();
                FindObjectOfType<PopUpSystem>().PopUp("+" + AdditionTime, transform.position);
                FindObjectOfType<GemsSpawner>().SpawnGems();
                StopCoroutine(DestroyCR);
                GetComponent<MeshRenderer>().enabled = false;
                Destroy(this.gameObject, 0.2f);

                //SFX
                audioSource.PlayOneShot(GemPopSound);

                //Particle Effect
                Instantiate(GemPopParticleEffect, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            }
        }
    }

    IEnumerator DestoryAfterLifetime()
    {
        yield return new WaitForSeconds(gemLifetime);
        //FindObjectOfType<GemsSpawner>().SpawnGems();
        //Destroy(this.gameObject);
    }
}
