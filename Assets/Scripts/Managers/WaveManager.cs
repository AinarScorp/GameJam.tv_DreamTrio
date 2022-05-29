using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveManager : MonoBehaviour
{
    [SerializeField] bool testStartWave;
    EnemySpawnerNew[] enemySpawners;
    
    int remainingEnemies;
    int enemiesTotal;
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
        remainingEnemies += amount;
    }
    public void RemoveFromWaveCount()
    {
        remainingEnemies--;
        int killedNumber = enemiesTotal - remainingEnemies;
        InterfaceManager.Instance.DisplayCurrentKillCount(killedNumber, enemiesTotal);

        StartNextWave();
    }

    bool ReadyForTheNextWave()
    {
        return remainingEnemies <= 0;
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
        remainingEnemies = 0;
        enemySpawners.ToList().ForEach(spawner => StartCoroutine(spawner.StartSpawningEnemies(currentWave)));
        enemiesTotal = remainingEnemies;
        InterfaceManager.Instance.DisplayCurrentKillCount(0, enemiesTotal);
        currentWave++;
        InterfaceManager.Instance.DisplayCurrentWaveNumber(currentWave);

    }
}
