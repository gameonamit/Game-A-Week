using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int HealthDecrease = 1;
    public GameObject smokeParticleEffect;
    public GameObject bombModel;

    private GameManager gameManager;
    private bool isTriggered = false;
    private bool destroyed = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        SimulatedGravity();
    }

    private void SimulatedGravity()
    {
        transform.position -= gameManager.Gravity * new Vector3(0, 1, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!destroyed)
        {
            destroyed = true;
            if (isTriggered == true)
            {
                ItemActivated();
            }
            InstantiateParticle();
            Destroy(this.gameObject, 3f);
        }
    }

    public void ItemActivated()
    {
        FindObjectOfType<HealthSys>().DecreaseHealth(HealthDecrease);
    }

    private void InstantiateParticle()
    {
        float yOffset = 0.8f;
        Vector3 spawnPosition = new Vector3(transform.position.x,
            transform.position.y - yOffset, transform.position.z);

        GameObject smokePE = Instantiate(smokeParticleEffect, spawnPosition, smokeParticleEffect.transform.rotation);
        smokePE.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DestroyParticleEffect(smokePE));

        bombModel.SetActive(false);
    }

    IEnumerator DestroyParticleEffect(GameObject gm)
    {
        yield return new WaitForSeconds(2f);
        Destroy(gm.gameObject);
    }

    public void ColliderTriggered(bool value)
    {
        isTriggered = value;
    }
}
