using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatGenerator : MonoBehaviour
{
    public static BeatGenerator instance;

    [SerializeField] GameObject beatPrefab;
    [SerializeField] GameObject bombPrefab;

    [SerializeField] Transform beatContent;
    [SerializeField] Transform bombContent;

    [SerializeField] int beatNumber;
    [SerializeField] int tempo;

    [SerializeField] float xPosOne, xPosTwo;
    [SerializeField] float yPos;
    [SerializeField] float startingZPos;

    public float activatedBeatCount = 0;
    public float beatCount;

    private bool lastPosOne = false;
    private float xPos;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        beatCount = beatContent.childCount;
        ProgressSystem.instance.UpdateProgressText();
    }

    public void GenerateBeats()
    {
        float zPos = startingZPos;
        for (int i = 0; i < beatNumber; i++)
        {
            if(lastPosOne == true)
            {
                int ran = Random.Range(0 , 101);
                if(ran <= 40)
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
                if (ran <= 40)
                {
                    xPos = xPosTwo;
                    lastPosOne = false;
                }
                else
                {
                    xPos = xPosOne;
                    lastPosOne = true;
                }
            }

            Vector3 beatPosition = new Vector3(xPos, yPos, zPos);
            Vector3 bombPosition = new Vector3(-xPos, yPos, zPos);
            GameObject beat = Instantiate(beatPrefab, beatPosition, Quaternion.identity);
            GameObject bomb = Instantiate(bombPrefab, bombPosition, Quaternion.identity);

            beat.transform.parent = beatContent;
            bomb.transform.parent = bombContent;

            zPos += tempo;
        }
    }

    public void ClearAllBeatObjects()
    {
        for(int i = 0; i < beatContent.transform.childCount; i++)
        {
            DestroyImmediate(beatContent.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < bombContent.transform.childCount; i++)
        {
            DestroyImmediate(bombContent.transform.GetChild(i).gameObject);
        }
    }

    public void AddActivatedBeatCount()
    {
        activatedBeatCount += 1f;
        ProgressSystem.instance.UpdateProgressText();
    }

    public void DisableAllBeat()
    {
        beatContent.gameObject.SetActive(false);
        bombContent.gameObject.SetActive(false);
    }
}
