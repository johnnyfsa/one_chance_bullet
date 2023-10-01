using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItemChance
{
    public GameObject Item;
    public float Chance;
}

public class DropItem : MonoBehaviour
{
    [SerializeField] List<DropItemChance> dropItems;

    public void Drop()
    {
        var totalChance = 0.0f;
        foreach (var item in dropItems)
        {
            totalChance += item.Chance;
        }
        var randomValue = UnityEngine.Random.Range(0.0f, totalChance);
        var currentChance = 0.0f;
        foreach (var item in dropItems)
        {
            currentChance += item.Chance;
            if (randomValue <= currentChance)
            {
                Pool.CreateObject(item.Item, transform.position, Quaternion.identity);
                return; 
            }
        }
    }
}
