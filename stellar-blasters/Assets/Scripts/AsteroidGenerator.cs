using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    public float spawnRange;
    public float amountToSpawn = 800;
    private Vector3 spawnPoint;
    [SerializeField]GameObject asteroid;
    public float startSafeRange;
    private List<GameObject> objectsToPlace = new List<GameObject>();

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
        if(difficulty.Equals("Easy"))
        {
            amountToSpawn = 800;
        }
        else if(difficulty.Equals("Medium"))
        {
            amountToSpawn *= 3;
        }
        else
        {
            amountToSpawn *= 5;
        }

        for (int i = 0; i < amountToSpawn; i++) 
        {
            PickSpawnPoint();

            //pick new spawn point if too close to player start
            while (Vector3.Distance(spawnPoint, Vector3.zero) < startSafeRange)
            {
                PickSpawnPoint();
            }

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
            spawnPoint.Normalize();
        }

        spawnPoint *= spawnRange;
    }
}

