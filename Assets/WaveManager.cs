using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveManager : MonoBehaviour
{
    [SerializeField] bool testStartWave;
    EnemySpawnerNew[] enemySpawners;
    List<EnemyHealth> activeEnemies = new List<EnemyHealth>();

    int currentWave = 0;
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
    public void AddActiveEnemies(EnemyHealth enemyToAdd)
    {
        activeEnemies.Add(enemyToAdd);
    }

    public void RemoveFromActiveEnemies(EnemyHealth enemyToRemove)
    {
        activeEnemies.Remove(enemyToRemove);
        StartNextWave();
    }

    bool ReadyForTheNextWave()
    {
        return activeEnemies.Count <= 0;
    }
    void StartNextWave()
    {
        if (!ReadyForTheNextWave())
            return;
        if (currentWave >= numberOfWaves)
        {
            Debug.Log("went through all waves");
            return;
        }
        enemySpawners.ToList().ForEach(spawner => StartCoroutine(spawner.StartSpawningEnemies(currentWave)));
        currentWave++;
    }
}
