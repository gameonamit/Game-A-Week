using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombObject : MonoBehaviour
{
    Renderer []ren;
    Transform player;

    [SerializeField] float distanceToActivate;

    bool activated = false;

    private void Awake()
    {
        ren = GetComponentsInChildren<Renderer>();
        player = FindObjectOfType<FPlayerController>().transform;
    }

    private void Start()
    {
        foreach (Renderer re in ren)
        {
            re.enabled = false;
        }
    }

    private void Update()
    {
        float zDistance = transform.position.z - player.transform.position.z;
        if (zDistance < distanceToActivate)
        {
            if (activated == false)
            {
                activated = true;
                foreach (Renderer re in ren)
                {
                    re.enabled = true;
                }
            }
        }

        if (activated == true)
        {
            if (zDistance < -distanceToActivate)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Renderer re in ren)
            {
                re.enabled = false;
            }
            FiGameManager.instance.GameOver();
            Destroy(this.gameObject, 0.6f);
        }
    }
}
