using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    [SerializeField] private float AdditionTime = 2f;
    [SerializeField] private float gemLifetime = 3f;

    Coroutine DestroyCR;

    private void Start()
    {
        DestroyCR = StartCoroutine(DestoryAfterLifetime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Timer timer = FindObjectOfType<Timer>();
            timer.AddTime(AdditionTime);
            other.GetComponent<PlayerController>().IncreaseScale();
            StopCoroutine(DestroyCR);
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestoryAfterLifetime()
    {
        yield return new WaitForSeconds(gemLifetime);
        Destroy(this.gameObject);
    }
}
