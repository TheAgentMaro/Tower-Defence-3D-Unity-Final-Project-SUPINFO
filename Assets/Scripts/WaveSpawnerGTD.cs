using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class SpawnPointWaves
{
    public Transform spawnTransform;
    public Waves[] waves;
    public Transform[] spawnPoints;
}

public class WaveSpawnerGTD : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public SpawnPointWaves[] spawnPointWaves;

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
            waveCountdown = spawnPointWaves[currentWaveIndex].waves[0].WaveRate;
        }
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.rounds++;

        SpawnPointWaves currentSpawnPointWave = spawnPointWaves[currentWaveIndex];
        Waves[] waves = currentSpawnPointWave.waves;

        for (int i = 0; i < waves.Length; i++)
        {
            Waves currentWave = waves[i];

            for (int j = 0; j < currentWave.WaveCount; j++)
            {
                SpawnEnemy(currentWave.gameEnemie, currentSpawnPointWave.spawnPoints);
                yield return new WaitForSeconds(1f / currentWave.WaveRate);
            }
        }

        currentWaveIndex++;
        currentEnemyIndex = 0;
    }

    void SpawnEnemy(GameObject gameEnemie, Transform[] spawnPoints)
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned for enemy spawning.");
            return;
        }

        if (gameEnemie == null)
        {
            Debug.LogWarning("No enemy prefab assigned for spawning.");
            return;
        }

        int spawnIndex = currentEnemyIndex % spawnPoints.Length;
        GameObject spawnedEnemy = Instantiate(gameEnemie, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
        GameEnemyGTD enemyComponent = spawnedEnemy.GetComponent<GameEnemyGTD>();
        if (enemyComponent != null)
        {
            enemyComponent.SetWaypoints(spawnPoints);
        }
        enemiesAlive++;
        currentEnemyIndex++;
    }
}
