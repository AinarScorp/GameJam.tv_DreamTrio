using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave 
{
    public Chunk[] chunks;
    [Tooltip("if checked, chunks have the same period between spawns")]
    [HideInInspector]public bool sameTime = true;

    [Header("Contsant time between Chunks")]
    public float timeConstant;

    [Header("Various time between Chunks __ NOT WORKING")]
    [Tooltip("leave empty if same time is checked")]
    [HideInInspector] public float[] timeDifferent;
}

[System.Serializable]
public class Chunk
{
    [Tooltip("Add every enemy you want to be spawned in this chunk")]
    public List<EnemyHealth> enemies = new List<EnemyHealth>();

    public bool spawnAtOnce;

    [Header("Various time between enemy spawns in this chunk")]
    [Tooltip("leave empty if spawnAtOnce is checked")]
    public float spawnInteval;

    public EnemyHealth OneEnemy()
    {
        int rndEnemy = Random.Range(0, enemies.Count);
        EnemyHealth enemy = enemies[rndEnemy];
        enemies.RemoveAt(rndEnemy);
        return enemy;
    }

    public List<EnemyHealth> AllEnemies()
    {
        return enemies;
    }
}

