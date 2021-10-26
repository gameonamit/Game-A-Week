using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatObject : MonoBehaviour
{
    Renderer ren;
    [SerializeField] float distanceToActivate;
    [SerializeField] int score = 20;

    Transform player;
    bool activated = false;

    private void Awake()
    {
        ren = GetComponentInChildren<Renderer>();     
    }

    private void Start()
    {
        player = FiGameManager.instance.GetPlayer();
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
                BeatGenerator.instance.AddActivatedBeatCount();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FiGameManager.instance.PlaySongBeat();
            BeatGenerator.instance.AddActivatedBeatCount();
            FScoringSystem.instance.AddScore(score);
            ren.enabled = false;
            Destroy(this.gameObject, 0.6f);
        }
    }
}
