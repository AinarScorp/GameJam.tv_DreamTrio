using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


public class WaveManager : MonoBehaviour
{
    [SerializeField] float waveStartTimer = 5f;
    [SerializeField] TextMeshProUGUI clockDown;
    [SerializeField] int currentWave = 0;


    EnemySpawnerNew[] enemySpawners;
    PlayerPoison poison;
    int remainingEnemies;
    int enemiesTotal;
    int numberOfWaves;
    private void Awake()
    {
        enemySpawners = GetComponentsInChildren<EnemySpawnerNew>();
        poison = FindObjectOfType<PlayerPoison>();
    }

    private void Start()
    {
        CalculateNumberOfWaves();
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

        if (ReadyForTheNextWave())
            NextWave();
    }

    bool ReadyForTheNextWave()
    {
        return remainingEnemies <= 0;
    }

    public void NextWave()
    {
        StartCoroutine(StartNextWave());

    }
    IEnumerator StartNextWave()
    {
        poison.AbruptPoisonCount();
        if (currentWave >= numberOfWaves)
        {
            FindObjectOfType<PlayerManager>().StartGameOver(true);
            Debug.LogWarning("you have won");
            yield break;
        }
        yield return WaveTimerStart();

        remainingEnemies = 0;
        enemySpawners.ToList().ForEach(spawner => StartCoroutine(spawner.StartSpawningEnemies(currentWave)));
        enemiesTotal = remainingEnemies;
        InterfaceManager.Instance.DisplayCurrentKillCount(0, enemiesTotal);
        currentWave++;
        InterfaceManager.Instance.DisplayCurrentWaveNumber(currentWave, numberOfWaves);
        poison.StartPoisonCount();

    }

    IEnumerator WaveTimerStart()
    {
        clockDown.gameObject.SetActive(true);
        float timeRemaining = waveStartTimer;

        while (timeRemaining > 0.1f)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTime(timeRemaining);
            yield return null;
        }

        clockDown.gameObject.SetActive(false);

    }
    void UpdateTime(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //float miliSeconds = Mathf.FloorToInt(timeToDisplay * 1000f % 1000);

        clockDown.text = string.Format("{0:0}", seconds);
    }
}
