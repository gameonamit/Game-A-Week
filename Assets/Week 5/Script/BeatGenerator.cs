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

    [SerializeField] public List<Vector3> beatAllItems;
    [SerializeField] public List<Vector3> bombAllItems;

    private bool[] beatActivated;
    private bool[] bombActivated;

    [SerializeField] private float distanceToActivate = 80f;

    private bool lastPosOne = false;
    private float xPos;

    private Transform player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        beatCount = beatAllItems.Count;
        ProgressSystem.instance.UpdateProgressText();
        player = FiGameManager.instance.GetPlayer();
        beatActivated = new bool[beatAllItems.Count];
        bombActivated = new bool[bombAllItems.Count];

        for (int i = 0; i < beatActivated.Length; i++)
        {
            beatActivated[i] = false;
        }

        for (int i = 0; i < bombActivated.Length; i++)
        {
            bombActivated[i] = false;
        }
    }

    private void Update()
    {
        //Beat
        for(int i = 0; i<beatAllItems.Count; i++)
        {
            float distance = beatAllItems[i].z - player.position.z;
            if (distance < distanceToActivate)
            {
                if(beatActivated[i] == false)
                {
                    beatActivated[i] = true;
                    SpawnBeatObject(beatAllItems[i]);
                }
            }
        }

        //Bomb
        for (int i = 0; i < bombAllItems.Count; i++)
        {
            float distance = bombAllItems[i].z - player.position.z;
            if (distance < distanceToActivate)
            {
                if (bombActivated[i] == false)
                {
                    bombActivated[i] = true;
                    SpawnBombObject(bombAllItems[i]);
                }
            }
        }
    }

    private void SpawnBeatObject(Vector3 position)
    {
        Instantiate(beatPrefab, position, Quaternion.identity);
    }

    private void SpawnBombObject(Vector3 position)
    {
        Instantiate(bombPrefab, position, Quaternion.identity);
    }

    public void GenerateBeats()
    {
        float zPos = startingZPos;
        for (int i = 0; i < beatNumber; i++)
        {
            if (lastPosOne == true)
            {
                int ran = Random.Range(0, 101);
                if (ran <= 40)
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
        for (int i = 0; i < beatContent.transform.childCount; i++)
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

    public void SaveAllBeatObjects()
    {
        for (int i = 0; i < beatContent.childCount; i++)
        {
            beatAllItems.Add(beatContent.GetChild(i).transform.position);
        }
    }

    public void SaveAllBombObjects()
    {
        for (int i = 0; i < beatContent.childCount; i++)
        {
            bombAllItems.Add(bombContent.GetChild(i).transform.position);
        }
    }

    public void ClearAllBeatLists()
    {
        beatAllItems.Clear();
    }

    public void ClearAllBombLists()
    {
        bombAllItems.Clear();
    }
}
