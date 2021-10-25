using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatGenerator : MonoBehaviour
{
    [SerializeField] GameObject beatPrefab;
    [SerializeField] Transform beatContent;
    [SerializeField] int beatNumber;
    [SerializeField] int tempo;

    [SerializeField] float xPosOne, xPosTwo;
    [SerializeField] float yPos;
    [SerializeField] float startingZPos;

    public float activatedBeatCount = 0;
    public float beatCount;

    private bool lastPosOne;
    private float xPos;

    [SerializeField] private ProgressSystem progressSys;

    private void Start()
    {
        beatCount = beatContent.childCount;
        progressSys.UpdateProgressText();
    }

    public void GenerateBeats()
    {
        float zPos = startingZPos;
        for (int i = 0; i < beatNumber; i++)
        {
            if(lastPosOne == true)
            {
                int ran = Random.Range(0 , 101);
                if(ran < 15)
                {
                    xPos = xPosOne;
                    lastPosOne = true;
                }
                else
                {
                    xPos = xPosTwo;
                    lastPosOne = false;
                }
            }
            else
            {
                int ran = Random.Range(0, 101);
                if (ran < 15)
                {
                    xPos = xPosTwo;
                    lastPosOne = false;
                }
                else
                {
                    xPos = xPosTwo;
                    lastPosOne = true;
                }
            }

            Vector3 position = new Vector3(xPos, yPos, zPos);
            GameObject beat = Instantiate(beatPrefab, position, Quaternion.identity);
            beat.transform.parent = beatContent;

            zPos += tempo;
        }
    }

    public void ClearAllBeatObjects()
    {
        for(int i = 0; i < beatContent.transform.childCount; i++)
        {
            DestroyImmediate(beatContent.transform.GetChild(i).gameObject);
        }
    }

    public void AddActivatedBeatCount()
    {
        activatedBeatCount += 1f;
        progressSys.UpdateProgressText();
    }
}
