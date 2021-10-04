using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemTypes itemType;
    private GameManager gameManager;

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
        if (other.gameObject.CompareTag("Player"))
        {
            ItemActivated();
        }
        Destroy(this.gameObject);
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
