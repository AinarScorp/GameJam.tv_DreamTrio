using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawnerNew : MonoBehaviour
{
    [SerializeField] bool spanwVertically = false;
    [SerializeField] float spawnLineLength = 2f;
    [SerializeField] Wave[] waves;

    WaveManager waveManager;

    public Wave[] Waves { get => waves; }

    private void Awake()
    {
        waveManager = GetComponentInParent<WaveManager>();
    }

    public IEnumerator StartSpawningEnemies(int currentWaveNumber)
    {
        if (currentWaveNumber +1 > waves.Length)
            yield break;

        Wave currentWave = waves[currentWaveNumber];
        currentWave.chunks.ToList().ForEach(chunk => waveManager.AddAmountOfEnemies(chunk.AllEnemies().Count));


        if (currentWave.sameTime)
        {
            foreach (Chunk chunk in currentWave.chunks)
            {
                if (chunk.spawnAtOnce)
                {
                    //that spawns all enemies in a chunk at the same time 

                    List<EnemyHealth> enemiesToSpawn = chunk.AllEnemies();
                    enemiesToSpawn.ToList().ForEach(enemy =>
                    {
                        //float xPos = Random.Range(transform.position.x, lineToSpawn.x);
                        EnemyHealth enemyHealth = Instantiate(enemy, GetSpawnPosition(), Quaternion.identity, transform);
                    });
                }
                else
                {
                    //this spawns  enemies in a chunk with an interval between them

                    int numberOfEnemies = chunk.AllEnemies().Count;
                    for (int i = 0; i < numberOfEnemies; i++)
                    {
                        //float xPos = Random.Range(transform.position.x, lineToSpawn.x);

                        EnemyHealth enemyHealth = Instantiate(chunk.OneEnemy(), GetSpawnPosition(), Quaternion.identity, transform);
                        yield return new WaitForSeconds(chunk.spawnInteval);
                    }

                }

            }
            yield return new WaitForSeconds(currentWave.timeConstant);
        }

    }

    Vector3 GetSpawnPosition()
    {
        if (spanwVertically)
        {
            float yPos = Random.Range(transform.position.y, transform.position.y + spawnLineLength);

            return new Vector3(transform.position.x, yPos);
        }
        else
        {
            float xPos = Random.Range(transform.position.x, transform.position.x + spawnLineLength);

            return new Vector3(xPos, transform.position.y);
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        if (spanwVertically)
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + spawnLineLength));

        else
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x+ spawnLineLength, transform.position.y ));

    }
}
