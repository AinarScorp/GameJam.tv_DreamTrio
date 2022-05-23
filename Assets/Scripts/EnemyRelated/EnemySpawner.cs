using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    
    [Tooltip("Where you want to sort them")]
    [SerializeField] Transform parentForSpanws;

    [Tooltip("Attach an object here you desire to spawn")]
    [SerializeField] GameObject spawnObject;

    [SerializeField] [Range(0,20)]float secondsBetweenSpawns;

    Coroutine continiouslySpawnEnemies;

    [SerializeField] [HideInInspector] bool enemiesSpawn = false;


    public void StartSpawningEnemies()
    {
        continiouslySpawnEnemies = StartCoroutine(SpawnEnemies());
        enemiesSpawn = true;
    }

    public void StopSpawningEnemies()
    {
        enemiesSpawn = false;
        if (continiouslySpawnEnemies == null)
        {
            return;
        }

        StopCoroutine(continiouslySpawnEnemies);
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Instantiate(spawnObject, this.transform.position, Quaternion.identity, parentForSpanws);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }


}
