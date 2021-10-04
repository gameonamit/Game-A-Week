using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwoPopUpSys : MonoBehaviour
{
    [SerializeField] private GameObject PopUpCanvasPrefab;

    [SerializeField] private float rotationDegree = 30;
    [SerializeField] private float destoryTime = 1;

    [SerializeField] private float offSetMin = 0f;
    [SerializeField] private float offSetMax = 0.8f;

    public void PopUp(string Txt, Vector3 location, float dir)
    {
        GameObject PopUpGM = Instantiate(PopUpCanvasPrefab, location, Quaternion.identity);

        float ran = Random.Range(-rotationDegree, rotationDegree);
        PopUpGM.transform.eulerAngles = new Vector3(0, 0, ran);

        //Applying Offset position to toast gm
        PopUpApplyOffset(PopUpGM, dir);

        PopUpGM.GetComponentInChildren<TextMeshProUGUI>().text = Txt;
        StartCoroutine(PopUpReset(PopUpGM));
    }

    private void PopUpApplyOffset(GameObject PopUpGM, float Dir)
    {
        Vector2 PopUpOffset;

        if (Dir == 0)
        {
            PopUpOffset = new Vector2(offSetMin, offSetMax);
        }
        else
        {
            PopUpOffset = new Vector2(-offSetMin, -offSetMax);
        }

        float randomOffSet = Random.Range(PopUpOffset.x, PopUpOffset.y);
        PopUpGM.transform.position += new Vector3(randomOffSet, 0f, 0f);
    }

    private IEnumerator PopUpReset(GameObject gm)
    {
        yield return new WaitForSeconds(destoryTime);
        Destroy(gm.gameObject);
    }
}
