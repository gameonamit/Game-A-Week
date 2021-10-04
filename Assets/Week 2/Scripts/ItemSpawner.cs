using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float MinXPos;
    [SerializeField] private float MaxXPos;
    [SerializeField] private float YPos;
    [SerializeField] private float ZPos;

    [SerializeField] private float SpawnFrequency = 2f;
    private float time;

    public ItemsGroup ItemsGroup;

    private void Start()
    {
        SetRandomTime();
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            SpawnItem();
            SetRandomTime();
        }
    }

    private void SetRandomTime()
    {
        time = Random.Range(0, SpawnFrequency);
    }

    public void SpawnItem()
    {
        //RandomItem
        int itemsCount = ItemsGroup.m_Items.Length;
        int randomItem = Random.Range(0, itemsCount);

        float randomXPos = Random.Range(MinXPos, MaxXPos);
        Vector3 SpawnPosition = new Vector3(randomXPos, YPos, ZPos);
        GameObject item = Instantiate(ItemsGroup.m_Items[randomItem], SpawnPosition, Quaternion.identity);
    }

    private void GetRandomItem()
    {

    }




}
