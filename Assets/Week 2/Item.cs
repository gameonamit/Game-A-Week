using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemTypes itemType;

    void FixedUpdate()
    {
        SimulatedGravity();
    }

    private void SimulatedGravity()
    {
        transform.position -= GameManager.Gravity * new Vector3(0, 1, 0) * Time.deltaTime;
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
        Vector2 PopUpOffset = new Vector2(-1, 1);
        Vector2 ToastOffset = new Vector2(-3, 3);

        FindObjectOfType<TwoPopUpSys>().PopUp("+ " + itemType.score.ToString(),
            transform.position, PopUpOffset.x, PopUpOffset.y);
        FindObjectOfType<TwoToastSys>().Toast(itemType.ToastMessage,
            transform.position, ToastOffset.x, ToastOffset.y);
        FindObjectOfType<TwoScoringSys>().IncreaseScore(itemType.score);
    }
}
