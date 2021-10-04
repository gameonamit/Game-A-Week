using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwoToastSys : MonoBehaviour
{
    [SerializeField] private GameObject ToastCanvasPrefab;

    [SerializeField] private float rotationDegree = 30;
    [SerializeField] private float destoryTime = 1f;

    [SerializeField] private float offSetMin = 0.5f;
    [SerializeField] private float offSetMax = 2f;

    public void Toast(string Txt, Vector3 location, float dir)
    {
        GameObject PopUpGM = Instantiate(ToastCanvasPrefab, location, Quaternion.identity);

        float ran = Random.Range(-rotationDegree, rotationDegree);
        PopUpGM.transform.eulerAngles = new Vector3(0, 0, ran);

        //Applying Offset position to toast gm
        ToastApplyOffset(PopUpGM, dir);

        PopUpGM.GetComponentInChildren<TextMeshProUGUI>().text = Txt;
        StartCoroutine(PopUpReset(PopUpGM));
    }

    private void ToastApplyOffset(GameObject PopUpGM, float Dir)
    {
        Vector2 ToastOffset;

        if(Dir == 0)
        {
            ToastOffset = new Vector2(offSetMin, offSetMax);
        }
        else
        {
            ToastOffset = new Vector2(-offSetMax, -offSetMin);
        }

        float randomOffSet = Random.Range(ToastOffset.x, ToastOffset.y);
        PopUpGM.transform.position += new Vector3(randomOffSet, 0f, 0f);
    }

    private IEnumerator PopUpReset(GameObject gm)
    {
        yield return new WaitForSeconds(destoryTime);
        Destroy(gm.gameObject);
    }
}
