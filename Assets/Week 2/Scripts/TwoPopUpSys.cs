using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwoPopUpSys : MonoBehaviour
{
    [SerializeField] private GameObject PopUpCanvasPrefab;

    [SerializeField] private float rotationDegree = 30;
    [SerializeField] private float destoryTime = 1;

    public void PopUp(string Txt, Vector3 location, float offMin, float offMax)
    {
        GameObject PopUpGM = Instantiate(PopUpCanvasPrefab, location, Quaternion.identity);

        float ran = Random.Range(-rotationDegree, rotationDegree);
        PopUpGM.transform.eulerAngles = new Vector3(0, 0, ran);

        float randomOffSet = Random.Range(offMin, offMax);
        PopUpGM.transform.position += new Vector3(randomOffSet, 0f, 0f);

        PopUpGM.GetComponentInChildren<TextMeshProUGUI>().text = Txt;
        StartCoroutine(PopUpReset(PopUpGM));
    }

    private IEnumerator PopUpReset(GameObject gm)
    {
        yield return new WaitForSeconds(destoryTime);
        Destroy(gm.gameObject);
    }
}
