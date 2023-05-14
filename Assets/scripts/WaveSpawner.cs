using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public Waves[] waveT;

    [SerializeField]
    private float timeBwaves = 5.5f;

    [SerializeField]
    private Transform SpawnPoint;

    private float WaveCountdown = 5f;

    [SerializeField]
    private Text WaveTimer;

    public int WaveCount = 0;
    // Update is called once per frame
    void Update()
    {
        if (enemiesAlive > 0)
        {
            return;
        }

        WaveCountdown -= Time.deltaTime;
        WaveCountdown = Mathf.Clamp(WaveCountdown, 0f, Mathf.Infinity);
        WaveTimer.text = string.Format("{0:00.00}", WaveCountdown);

        if (WaveCountdown <= 0)
        {
            StartCoroutine(SpawnWave());
            WaveCountdown = timeBwaves;
        }
    }
    IEnumerator SpawnWave()
    {
        
        PlayerStats.rounds++;

        Waves tmp = waveT[WaveCount];
        for (int i = 0; i < tmp.WaveCount; i++)
        {
            SpawnEnemie(tmp.gameEnemie);
            yield return new WaitForSeconds(1f/tmp.WaveRate);
        }
        WaveCount++;
    }
    void SpawnEnemie(GameObject gameEnemie)
    {
        Instantiate(gameEnemie, SpawnPoint.position,SpawnPoint.rotation);
        enemiesAlive++;
    }
}
