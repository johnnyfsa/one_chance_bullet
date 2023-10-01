using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnLoactions;
    [SerializeField] EnemyWave[] wavePresets;

    [SerializeField] float delayBetweenWaves = 2f;
    [SerializeField] int numberOfWaves = 3;
    [SerializeField] float waveDuration = 30f;
    [SerializeField] int initialWavePoints;
    [SerializeField] int aditionalPointsPerWave;

    int currentWave = 0;
    int currentWavePoints = 0;
    int enemiesCount = 0;
    int wavePresetIndex = 0;
    int spawnLocationIndex = 0;

    private void Start() {
        Init();
    }

    public void Init() {
        currentWave = 0;
        currentWavePoints = initialWavePoints;
        wavePresetIndex = 0;

        InitCurrentWave();
    }

    public void StartNextWave() {
        currentWave++;

        if(numberOfWaves > 0 && currentWave >= numberOfWaves) {
            GameOver();
        }

        currentWavePoints += aditionalPointsPerWave;
        wavePresetIndex = (wavePresetIndex + 1) % wavePresets.Length;

        InitCurrentWave();
    }

    void InitCurrentWave() {
        wavePresets[wavePresetIndex].Init(currentWavePoints, waveDuration);
        
        // Podem ter sobrado inimigos da wave anterior
        enemiesCount += wavePresets[wavePresetIndex].EnemyCount;

        StartCoroutine(SpawnWaveCO(wavePresets[wavePresetIndex]));
    }

    Enemy NextEnemy() 
    {
        return wavePresets[wavePresetIndex].NextEnemy();
    }

    void SpawnEnemy(Enemy enemy) {
        var spawnLocation = spawnLoactions[spawnLocationIndex];
        var enemyObject = Pool.CreateObject(enemy.gameObject, spawnLocation.position, Quaternion.identity);
        
        enemyObject.GetComponent<Health>().OnDeath -= DecreaseEnemyCount;
        enemyObject.GetComponent<Health>().OnDeath += DecreaseEnemyCount;
        
        spawnLocationIndex = (spawnLocationIndex + 1) % spawnLoactions.Length;
    }

    public void DecreaseEnemyCount() {
        enemiesCount--;
    }

    IEnumerator SpawnWaveCO(EnemyWave wave) {
        yield return new WaitForSeconds(delayBetweenWaves);

        StartCoroutine(NextWaveCO());
        
        while(true) {
            Enemy enemy = NextEnemy();
            
            if (enemy == null) {
                yield break;
            }

            SpawnEnemy(enemy);
            yield return new WaitForSeconds(wave.SpawnInterval);
        }
    }

    IEnumerator NextWaveCO() {
        float time = 0.0f;
        while(time < waveDuration) {
            yield return null;
            time += Time.deltaTime;

            if(enemiesCount == 0) {
                yield return new WaitForSeconds(Mathf.Min(delayBetweenWaves, waveDuration - time));
                break;
            }
        }

        StartNextWave();
    }

    void GameOver() {
        GameManager.Instance.GameOver();
    }

    // Select The next Wave Preset and Init it, the wave end after a time
    // Select the next spawn location and spawn the next enemy from the wave preset. Stop spawning when there is no next enemy
    // Wait a interval to spawn the next enemy
    // When all enemies are dead, select the next Wave Preset and Init it after a time
}
