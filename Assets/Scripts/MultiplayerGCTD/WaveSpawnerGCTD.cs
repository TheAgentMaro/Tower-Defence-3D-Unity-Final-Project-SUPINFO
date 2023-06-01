using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;



public class WaveSpawnerGCTD : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public Waves[] waveT;

    [SerializeField]
    private float timeBwaves = 5.5f;

    public Transform[] spawnPoints;

    private float waveCountdown = 5f;

    [SerializeField]
    private Text waveTimer;

    public int waveCount = 0;

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
            waveCountdown = timeBwaves;
        }
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.rounds++;

        Waves currentWave = waveT[waveCount];
        for (int i = 0; i < currentWave.WaveCount; i++)
        {
            SpawnEnemy(currentWave.gameEnemie);
            yield return new WaitForSeconds(1f / currentWave.WaveRate);
        }
        waveCount++;
    }

    void SpawnEnemy(GameObject gameEnemie)
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(gameEnemie, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
        enemiesAlive++;
    }
}