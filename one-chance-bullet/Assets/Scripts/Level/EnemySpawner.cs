using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyChance {
    public GameObject Prefab;
    public float Chance;
}

// class that instantiate a new enemy from the list of enemies with a chance on a interval. The interval decreases over time to a minimum value.
public class EnemySpawner : MonoBehaviour
{
    // class that instantiate a new enemy from the list of enemies with a chance on a interval. The interval decreases over time to a minimum value.
    [SerializeField] List<EnemyChance> enemies;
    [SerializeField] float minInterval = 1.0f;
    [SerializeField] float maxInterval = 5.0f;
    [SerializeField] float intervalDecrease = 0.1f;
    [SerializeField] float intervalDecreaseInterval = 5.0f;

    float interval = 0.0f;

    private void OnEnable() {
        interval = maxInterval;
        StartCoroutine(SpawnEnemyCO());
        StartCoroutine(DecreaseIntervalCO());
    }

    private IEnumerator DecreaseIntervalCO()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalDecreaseInterval);

            interval -= intervalDecrease;
            if (interval < minInterval)
            {
                interval = minInterval;
                yield break;
            }
        }
    }

    //Coroutine to instantiate a new enemy from the list of enemies with a chance on a interval. The interval decreases over time to a minimum value.
    private IEnumerator SpawnEnemyCO()
    {
        while (true)
        {
            var enemy = GetRandomEnemy();
            if (enemy != null)
            {
                Pool.CreateObject(enemy, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(interval);
        }
    }

    private GameObject GetRandomEnemy() {
        var totalChance = 0.0f;
        foreach (var enemy in enemies)
        {
            totalChance += enemy.Chance;
        }
        var randomValue = UnityEngine.Random.Range(0.0f, totalChance);
        var currentChance = 0.0f;
        foreach (var enemy in enemies)
        {
            currentChance += enemy.Chance;
            if (randomValue <= currentChance)
            {
                return enemy.Prefab;
            }
        }
        return null;
    }
}
