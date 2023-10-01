using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class EnemyWave
{
    [SerializeField] string description;
    [SerializeField] List<Enemy> enemies;
    [SerializeField] float increaseInChance = 0.2f;
    [SerializeField] float spawnProportionTime = 0.5f;

    int wavePoints;
    float spawnInterval;
    
    public float SpawnInterval => spawnInterval;
    public int EnemyCount => enemySequence.Count;

    public void Init(int wavePoints, float waveDuration)
    {
        this.wavePoints = wavePoints;

        currentEnemyIndex = 0;
        enemySequence = new List<Enemy>();

        GenerateEnemySequence();
        CalculateIntervalBetweenEnemies(waveDuration);
    }

    void CalculateIntervalBetweenEnemies(float waveDuration) {
        spawnInterval = waveDuration * spawnProportionTime / enemySequence.Count;
    }

    public Enemy NextEnemy() {
        if(enemySequence.Count == 0 || currentEnemyIndex >= enemySequence.Count)
        {
            return null;
        }
        var enemy = enemySequence[currentEnemyIndex];
        currentEnemyIndex++;
        return enemy;
    }

    void GenerateEnemySequence() {
        while (wavePoints > 0)
        {
            var enemy = GetRandomEnemy();
            enemySequence.Add(enemy);
            wavePoints -= enemy.SpawnValue;
        }
    }

    Enemy GetRandomEnemy()
    {        
        float totalPoints = enemies.Sum(enemy => EnemyChance(enemy));
        float randomValue = Random.Range(0, totalPoints);
        float currentPoints = 0.0f;
        foreach (var enemy in enemies)
        {
            currentPoints += EnemyChance(enemy);
            if (randomValue <= currentPoints)
            {
                return enemy;
            }
        }
        return null;
    }

    int currentEnemyIndex = 0;
    List<Enemy> enemySequence = new List<Enemy>();

    //The chance for each enemy to be selected is inversely proportional to its spawn value plus the increase in chance, so each enemy can have a good chance to be selected.
    float EnemyChance(Enemy enemy) => (1 / (enemy.SpawnValue + 0.01f)) + increaseInChance;
    
}
