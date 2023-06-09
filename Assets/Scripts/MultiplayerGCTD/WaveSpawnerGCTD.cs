using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class WaveWayPoint
{
    public Waves[] waves;
    public List<Transform> waypoints;
}

[System.Serializable]
public class WaveSpawningPoint
{
    public List<WaveWayPoint> spawnPoints;
}

public class WaveSpawnerGCTD : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public List<SpawningPoint> spawningPoints;
    public int remainingEnemiesInSpawnPoint;


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
            waveCountdown = GetNextWaveRate();
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

                currentSpawningPoint.remainingEnemiesInSpawnPoint = 0;

                for (int j = 0; j < spawnPoints.Count; j++)
                {
                    SpawnPoint currentSpawnPoint = spawnPoints[j];
                    Waves[] waves = currentSpawnPoint.waves;

                    for (int k = 0; k < waves.Length; k++)
                    {
                        Waves currentWave = waves[k];

                        if (l < currentWave.waveCount)
                        {
                            Transform waypoint = currentSpawnPoint.waypoints[Random.Range(0, currentSpawnPoint.waypoints.Count)];

                            for (int m = 0; m < currentWave.waveCount; m++)
                            {
                                GameObject gameEnemy = currentWave.gameEnemie;
                                if (gameEnemy != null)
                                {
                                    Coroutine spawnCoroutine = StartCoroutine(SpawnEnemy(gameEnemy, waypoint, currentSpawnPoint, currentSpawningPoint));

                                    spawnCoroutines.Add(spawnCoroutine);
                                    currentSpawningPoint.remainingEnemiesInSpawnPoint++;
                                    
                                    float delayTime = 0.25f; // Set the base delay time

                                    yield return new WaitForSeconds(delayTime);
                                }
                            }
                            PlayerStats.IncrementWaves();
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
        currentEnemyIndex++;
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
                    if (wave.waveCount > maxWaveCount)
                    {
                        maxWaveCount = wave.waveCount;
                    }
                }
            }
        }

        return maxWaveCount;
    }

    float GetNextWaveRate()
    {
        if (currentWaveIndex < spawningPoints.Count)
        {
            SpawningPoint currentSpawningPoint = spawningPoints[currentWaveIndex];
            List<SpawnPoint> spawnPoints = currentSpawningPoint.spawnPoints;

            if (currentEnemyIndex < spawnPoints.Count)
            {
                SpawnPoint currentSpawnPoint = spawnPoints[currentEnemyIndex];
                Waves[] waves = currentSpawnPoint.waves;

                if (currentEnemyIndex < waves.Length)
                {
                    return waves[currentEnemyIndex].waveRate;
                }
            }
        }

        return 0f;
    }

    IEnumerator SpawnEnemy(GameObject gameEnemy, Transform waypoint, SpawnPoint currentSpawnPoint, SpawningPoint spawningPoint)
    {
        if (gameEnemy == null)
        {
            Debug.LogWarning("No enemy prefab assigned for spawning.");
            yield break;
        }

        GameObject spawnedEnemy = Instantiate(gameEnemy, waypoint.position, waypoint.rotation);
        GameEnemyGCTD enemyComponent = spawnedEnemy.GetComponent<GameEnemyGCTD>();
        if (enemyComponent != null)
        {
            enemyComponent.SetWaypoints(currentSpawnPoint.waypoints.ToArray());
            enemyComponent.SetSpawner(this); // Pass the reference to the spawner
        }
        enemiesAlive++;
        currentEnemyIndex++;

        yield return null;
    }

    public void EnemyDiedInSpawnPoint(SpawningPoint spawningPoint)
    {
        spawningPoint.remainingEnemiesInSpawnPoint--;

        Debug.Log("Remaining enemies in spawn point: " + spawningPoint.remainingEnemiesInSpawnPoint);

        if (spawningPoint.remainingEnemiesInSpawnPoint <= 0)
        {
            PlayerStats.DecrementWaves();
        }
    }

}
