using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles enemy spawning in a 3D space, with adjustable difficulty settings and event-based control.
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    float spawnTimer = 7f;  // Time between spawns (changes with difficulty)
    public float spawnRange = 300f; // Maximum spawn distance from origin (300 units)
    private Vector3 spawnPoint = new Vector3(0f, 0f, 0f);
    public float startSafeRange = 10f; // Minimum safe distance from player (10 units)

    Vector3 GetRandomSpawnPoint()
    {
        // Generates random coordinates within a 150-unit cube (total range = 300 units).
        float randomX = Random.Range(-150f, 150f);
        float randomY = Random.Range(-150f, 150f);
        float randomZ = Random.Range(-150f, 150f);
        return new Vector3(randomX, randomY, randomZ);
    }

    void SpawnEnemy()
    {
        // Instantiation for spawning enemies
        Vector3 spawnPoint = GetRandomSpawnPoint();
        GameObject enemyObject = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }

    void OnEnable()
    {
        // Subscribes to game events
        EventManager.onStartGame += StartSpawning;
        EventManager.onPlayerDeath += StopSpawning;
    }

    void OnDisable()
    {
        // Unsubscribes when disabled (prevents memory leaks).
        StopSpawning();
        EventManager.onStartGame -= StartSpawning;
        EventManager.onPlayerDeath -= StopSpawning;
    }

    void StartSpawning(string mode, string difficulty)
    {
        // Triggered by EventManager.onStartGame
        spawnPoint = new Vector3(0.0f, 0.0f, 0.0f); // Reset spawn origin

        // Adjust spawn rate based on difficulty
        if (difficulty.Equals("Easy"))
        {
            spawnTimer = 4f;
        }
        else if (difficulty.Equals("Medium"))
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
        // Stops all scheduled spawns
        // Called on player death
        CancelInvoke();
    }

}
