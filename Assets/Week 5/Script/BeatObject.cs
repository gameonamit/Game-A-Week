using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatObject : MonoBehaviour
{
    Renderer ren;
    BeatGenerator beatGenerator;
    [SerializeField] float distanceToActivate;

    Transform player;
    bool activated = false;

    private void Awake()
    {
        ren = GetComponentInChildren<Renderer>();
        player = FindObjectOfType<FPlayerController>().transform;
        beatGenerator = FindObjectOfType<BeatGenerator>();
    }

    private void Start()
    {
        ren.enabled = false;
    }

    private void Update()
    {
        float zDistance = transform.position.z - player.transform.position.z;
        if(zDistance < distanceToActivate)
        {
            if (activated == false)
            {
                activated = true;
                ren.enabled = true;
            }
        }

        if(activated == true)
        {
            if(zDistance < -distanceToActivate)
            {
                beatGenerator.AddActivatedBeatCount();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FiGameManager.instance.PlaySongBeat();
            beatGenerator.AddActivatedBeatCount();
            ren.enabled = false;
            Destroy(this.gameObject, 0.6f);
        }
    }
}
