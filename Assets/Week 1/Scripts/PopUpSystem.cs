using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{
    [SerializeField] private GameObject PopUpGM;
    [SerializeField] private TextMeshProUGUI PopUpTxt;
    [SerializeField] private float rotationDegree = 30;

    private Coroutine PopUpResetCR;

    public void PopUp(string Txt, Vector3 location)
    {
        float ran = Random.Range(-rotationDegree, rotationDegree);
        PopUpGM.transform.eulerAngles = new Vector3(0, 0, ran);

        PopUpGM.transform.position = location;
        PopUpTxt.text = Txt;
        if(PopUpResetCR != null)
        {
            StopCoroutine(PopUpResetCR);
        }
        PopUpResetCR = StartCoroutine(PopUpReset());
    }

    private IEnumerator PopUpReset()
    {
        yield return new WaitForSeconds(2f);
        PopUpTxt.text = "";
    }
}
