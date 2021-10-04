using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemTypes itemType;
    private GameManager gameManager;

    public GameObject itemParticleEffect;
    private Renderer renderer;
    private SphereCollider collider;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<SphereCollider>();
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
        if (other.gameObject.CompareTag("Player"))
        {
            ItemActivated();
        }
        InstantiateParticle();
        renderer.enabled = false;
        collider.enabled = false;
        Destroy(this.gameObject, 3f);
    }

    public void ItemActivated()
    {
        //Apply PowerUps


        //Direction
        Vector2 direction = GetItemDirection();

        FindObjectOfType<TwoPopUpSys>().PopUp("+ " + itemType.score.ToString(),
            transform.position, direction.x);
        FindObjectOfType<TwoToastSys>().Toast(itemType.ToastMessage,
            transform.position, direction.y);
        FindObjectOfType<TwoScoringSys>().IncreaseScore(itemType.score);
    }

    private void PowerUps()
    {
        if (itemType.name == "Apple")
        {
            //Increase Player Size
        }
    }

    private void InstantiateParticle()
    {
        float yOffset = 0.8f;
        Vector3 spawnPosition = new Vector3(transform.position.x,
            transform.position.y - yOffset, transform.position.z);

        GameObject smokePE = Instantiate(itemParticleEffect, spawnPosition, itemParticleEffect.transform.rotation);
        smokePE.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DestroyParticleEffect(smokePE));
    }

    IEnumerator DestroyParticleEffect(GameObject gm)
    {
        yield return new WaitForSeconds(2f);
        Destroy(gm.gameObject);
    }

    private Vector2 GetItemDirection()
    {
        int random = Random.Range(0, 2);

        if(random == 0)
        {
            return new Vector2(0f, 1f);
        }
        else
        {
            return new Vector2(1f, 0f);
        }
    }
}
