using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class SpawnPointWaves
{
    public Transform spawnTransform;
    public Waves[] waves;
}

public class WaveSpawnerGTD : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public SpawnPoint[] spawnPoints;

    [SerializeField]
    private float timeBwaves = 5.5f;

    private float waveCountdown = 5f;

    [SerializeField]
    private Text waveTimer;

    public int waveCount = 0;

    void Update()
    {
        if (enemiesAlive > 0)
        {
            return;
        }

        waveCountdown -= Time.deltaTime;
        waveCountdown = Mathf.Clamp(waveCountdown, 0f, Mathf.Infinity);
        waveTimer.text = string.Format("{0:00.00}", waveCountdown);

        if (waveCountdown <= 0)
        {
            StartCoroutine(SpawnWave());
            waveCountdown = timeBwaves;
        }
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.rounds++;

        // Find the maximum number of waves among all spawn points
        int maxWaves = 0;
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            int numWaves = spawnPoint.waves.Length;
            maxWaves = Mathf.Max(maxWaves, numWaves);
        }

        // Create a list of coroutines for each spawn point
        List<Coroutine> spawnCoroutines = new List<Coroutine>();

        for (int waveIndex = 0; waveIndex < maxWaves; waveIndex++)
        {
            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                Waves[] waves = spawnPoint.waves;

                // Check if the current spawn point has the wave at the current index
                if (waveIndex < waves.Length)
                {
                    Waves wave = waves[waveIndex];
                    int totalEnemies = wave.WaveCount;
                    GameObject gameEnemy = wave.gameEnemie;
                    Transform spawnTransform = spawnPoint.spawnTransform;

                    // Start a coroutine for each spawn point to spawn enemies simultaneously
                    Coroutine spawnCoroutine = StartCoroutine(SpawnEnemiesAtSpawnPoint(totalEnemies, gameEnemy, spawnTransform, wave.WaveRate));
                    spawnCoroutines.Add(spawnCoroutine);
                }
            }

            // Wait until all spawn coroutines have finished
            yield return new WaitUntil(() => spawnCoroutines.All(c => c == null));

            // Clear the list of coroutines
            spawnCoroutines.Clear();
        }

        waveCount++;
    }

    IEnumerator SpawnEnemiesAtSpawnPoint(int totalEnemies, GameObject gameEnemy, Transform spawnTransform, float waveRate)
    {
        for (int j = 0; j < totalEnemies; j++)
        {
            if (CanSpawnEnemy(spawnTransform))
            {
                Instantiate(gameEnemy, spawnTransform.position, spawnTransform.rotation);
                enemiesAlive++;
            }
            else
            {
                yield return null; // Wait for the next frame if the spawn point is occupied
                j--; // Retry spawning the enemy at the same index
            }

            yield return new WaitForSeconds(1f / waveRate);
        }

        // Mark the coroutine as finished
        yield return null;
    }
    bool CanSpawnEnemy(Transform spawnTransform)
    {
        Collider[] colliders = Physics.OverlapSphere(spawnTransform.position, 1f); // Check for nearby colliders

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("gameEnemie")) // Check if there's already an enemy nearby
            {
                return false;
            }
        }

        return true;
    }
}