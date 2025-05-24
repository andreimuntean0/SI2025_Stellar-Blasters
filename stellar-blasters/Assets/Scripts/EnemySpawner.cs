using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    float spawnTimer = 7f;
    public float spawnRange= 300f;
    private Vector3 spawnPoint = new Vector3(0f, 0f, 0f);
    public float startSafeRange = 10f;

    Vector3 GetRandomSpawnPoint()
    {
        float randomX = Random.Range(-150f, 150f);
        float randomY = Random.Range(-150f, 150f);
        float randomZ = Random.Range(-150f, 150f);
        return new Vector3(randomX, randomY, randomZ);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPoint = GetRandomSpawnPoint();
        GameObject enemyObject = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }

    void OnEnable()
    {
        EventManager.onStartGame += StartSpawning;
        EventManager.onPlayerDeath += StopSpawning;
    }

    void OnDisable()
    {
        StopSpawning();
        EventManager.onStartGame -= StartSpawning;
        EventManager.onPlayerDeath -= StopSpawning;
    }

    void StartSpawning(string mode, string difficulty)
    {
        spawnPoint = new Vector3(0.0f, 0.0f, 0.0f);
        if(difficulty.Equals("Easy"))
        {
            spawnTimer = 4f;
        }
        else if(difficulty.Equals("Medium"))
        {
            spawnTimer = 3f;
        }
        else
        {
            spawnTimer = 1.5f;
        }
        InvokeRepeating("SpawnEnemy", spawnTimer, spawnTimer);
    }

    void StopSpawning()
    {
        CancelInvoke();
    }

}
