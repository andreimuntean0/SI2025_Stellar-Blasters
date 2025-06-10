using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    public float spawnRange;                // The radius of the spherical area in which asteroids can spawn.
    public float amountToSpawn = 800;       // The base number of asteroids to spawn, which increases based on difficulty.
    private Vector3 spawnPoint;             // A temporary variable used to store the calculated position for each asteroid.
    [SerializeField]GameObject asteroid;    // Reference to the asteroid prefab that will be instantiated.
    public float startSafeRange;            // Defines a "safe zone" around the player where no asteroids will spawn.
    private List<GameObject> objectsToPlace = new List<GameObject>();  // A list to keep track of all instantiated asteroids.

    void OnEnable()
    {
        EventManager.onStartGame += SpawnAsteroids;
    }

    void OnDisable()
    {
        EventManager.onStartGame -= SpawnAsteroids;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAsteroids(string mode, string difficulty)
    {
        // This method is triggered when the game starts. It adjusts the amountToSpawn based on the selected difficulty.
        if (difficulty.Equals("Easy"))
        {
            amountToSpawn = 800;
        }
        else if (difficulty.Equals("Medium"))
        {
            amountToSpawn *= 3;
        }
        else
        {
            amountToSpawn *= 5;
        }

        for (int i = 0; i < amountToSpawn; i++) 
        {
            PickSpawnPoint();  // to calculate a random position within a sphere.

            // pick new spawn point if too close to player start
            while (Vector3.Distance(spawnPoint, Vector3.zero) < startSafeRange)
            {
                PickSpawnPoint();
            }

            // It instantiates the asteroid at that location with a random rotation and stores it in the objectsToPlace list.
            // The asteroid’s parent is set to the generator for better hierarchy management in Unity.

            objectsToPlace.Add(Instantiate(asteroid, spawnPoint, Quaternion.Euler(Random.Range(0f,360f), Random.Range(0f, 360f), Random.Range(0f, 360f))));
            objectsToPlace[i].transform.parent = this.transform;
        }

        asteroid.SetActive(true);
    }

    public void PickSpawnPoint()
    {
        spawnPoint = new Vector3(
            Random.Range(-1f,1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f));

        if(spawnPoint.magnitude > 1)
        {
            // If the point is outside the unit sphere, normalize it to bring it back to the edge of the sphere.
            spawnPoint.Normalize();
        }

        spawnPoint *= spawnRange; // Scales the normalized vector to fit within the desired spawnRange.
    }
}

