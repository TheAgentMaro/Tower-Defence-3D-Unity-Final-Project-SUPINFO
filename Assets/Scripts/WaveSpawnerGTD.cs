using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class SpawnPoint
{
    public Waves[] waves;
    public List<Transform> waypoints;
}

[System.Serializable]
public class SpawningPoint
{
    public List<SpawnPoint> spawnPoints;
}

public class WaveSpawnerGTD : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public List<SpawningPoint> spawningPoints;

    private float waveCountdown = 5f;
    private int currentWaveIndex = 0;
    private int currentEnemyIndex = 0;

    [SerializeField]
    private Text waveTimer;

    // Update is called once per frame
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
            waveCountdown = spawningPoints[currentWaveIndex].spawnPoints[0].waves[0].WaveRate;
        }
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.rounds++;

        List<Coroutine> spawnCoroutines = new List<Coroutine>();

        // Get the maximum wave count among all spawn points
        int maxWaveCount = GetMaxWaveCount();

        for (int l = 0; l < maxWaveCount; l++)
        {
            for (int i = 0; i < spawningPoints.Count; i++)
            {
                SpawningPoint currentSpawningPoint = spawningPoints[i];
                List<SpawnPoint> spawnPoints = currentSpawningPoint.spawnPoints;

                for (int j = 0; j < spawnPoints.Count; j++)
                {
                    SpawnPoint currentSpawnPoint = spawnPoints[j];
                    Waves[] waves = currentSpawnPoint.waves;

                    for (int k = 0; k < waves.Length; k++)
                    {
                        Waves currentWave = waves[k];

                        if (l < currentWave.WaveCount)
                        {
                            Transform waypoint = currentSpawnPoint.waypoints[Random.Range(0, currentSpawnPoint.waypoints.Count)];

                            Coroutine spawnCoroutine = StartCoroutine(SpawnEnemy(currentWave.gameEnemie, waypoint, currentSpawnPoint));
                            spawnCoroutines.Add(spawnCoroutine);

                            // Delay between enemy spawns within a wave
                            if (k < waves.Length - 1 || l < currentWave.WaveCount - 1)
                            {
                                yield return new WaitForSeconds(currentWave.WaveRate);
                            }
                        }
                    }
                }
            }
        }

        foreach (Coroutine spawnCoroutine in spawnCoroutines)
        {
            yield return spawnCoroutine;
        }

        currentWaveIndex++;
        currentEnemyIndex = 0;
    }

    int GetMaxWaveCount()
    {
        int maxWaveCount = 0;

        for (int i = 0; i < spawningPoints.Count; i++)
        {
            SpawningPoint currentSpawningPoint = spawningPoints[i];
            List<SpawnPoint> spawnPoints = currentSpawningPoint.spawnPoints;

            for (int j = 0; j < spawnPoints.Count; j++)
            {
                SpawnPoint currentSpawnPoint = spawnPoints[j];
                Waves[] waves = currentSpawnPoint.waves;

                foreach (Waves wave in waves)
                {
                    if (wave.WaveCount > maxWaveCount)
                    {
                        maxWaveCount = wave.WaveCount;
                    }
                }
            }
        }

        return maxWaveCount;
    }


    IEnumerator SpawnEnemy(GameObject gameEnemy, Transform waypoint, SpawnPoint currentSpawnPoint)
    {
        if (gameEnemy == null)
        {
            Debug.LogWarning("No enemy prefab assigned for spawning.");
            yield break;
        }

        GameObject spawnedEnemy = Instantiate(gameEnemy, waypoint.position, waypoint.rotation);
        GameEnemyGTD enemyComponent = spawnedEnemy.GetComponent<GameEnemyGTD>();
        if (enemyComponent != null)
        {
            enemyComponent.SetWaypoints(currentSpawnPoint.waypoints.ToArray());
        }
        enemiesAlive++;
        currentEnemyIndex++;

        yield return null;
    }
}
