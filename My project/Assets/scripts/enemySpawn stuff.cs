using UnityEngine;

public class enemySpawnstuff : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 3f;
    void Start()
    {
        StartCoroutine(SpawnEnemiesRoutine());
    }
    IEnumerator SpawnEnemiesRoutine()
    {
        while (true) // Loop indefinitely for continuous spawning
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    { }
        // If you have multiple spawn points, choose one randomly
        Transform chosenSpawnPoint = transform; // Default to spawner's position
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            chosenSpawnPoint = spawnPoints[randomIndex];
        }

        // Instantiate the enemy prefab at the chosen spawn point's position and rotation
        Instantiate(enemyPrefab, chosenSpawnPoint.position, chosenSpawnPoint.rotation);

    }
