using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveManager : MonoBehaviour
{
    [SerializeField] bool testStartWave;
    EnemySpawnerNew[] enemySpawners;
    
    int numberOfEnemiesInTheWave;

    [SerializeField] int currentWave = 0;
    int numberOfWaves;
    private void Awake()
    {
        enemySpawners = GetComponentsInChildren<EnemySpawnerNew>();
    }

    private void Start()
    {
        CalculateNumberOfWaves();
    }
    private void Update()
    {
        if (testStartWave)
        {
            StartNextWave();
            testStartWave = false;
        }
    }

    void CalculateNumberOfWaves()
    {
        enemySpawners.ToList().ForEach(spawner => numberOfWaves = numberOfWaves < spawner.Waves.Length ? spawner.Waves.Length : numberOfWaves);
    }

    public void AddAmountOfEnemies(int amount)
    {
        numberOfEnemiesInTheWave += amount;
    }
    public void RemoveFromWaveCount()
    {
        numberOfEnemiesInTheWave--;
        StartNextWave();
    }

    bool ReadyForTheNextWave()
    {
        return numberOfEnemiesInTheWave <= 0;
    }
    void StartNextWave()
    {
        if (!ReadyForTheNextWave())
            return;
        if (currentWave >= numberOfWaves)
        {
            Debug.LogWarning("you have won");
            return;
        }
        numberOfEnemiesInTheWave = 0;
        enemySpawners.ToList().ForEach(spawner => StartCoroutine(spawner.StartSpawningEnemies(currentWave)));
        currentWave++;
    }
}
