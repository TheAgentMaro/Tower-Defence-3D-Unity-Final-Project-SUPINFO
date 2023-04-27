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
    private Transform spawnPoint;

    private float waveCountdown = 5f;

    [SerializeField]
    private Text waveTimer;

    private int waveCount = 0;
    // Update is called once per frame
    void Update()
    {
        if(enemiesAlive>0)
        {
            return;
        }
        if(waveCountdown>0)
        {
            waveCountdown -= Time.deltaTime;
            waveCountdown = Mathf.Clamp(waveCountdown, 0f, Mathf.Infinity);
            waveTimer.text = string.Format("{0:00.00}", waveCountdown);
        }
        else
        {
            StartCoroutine (spawnWave());
            waveCountdown = timeBwaves;
        }
    }
    IEnumerator spawnWave()
    {
        
        PlayerStats.rounds++;

        Waves tmp = waveT[waveCount];
        for (int i = 0; i < tmp.waveCount; i++)
        {
            SpawnEnemie(tmp.gameEnemy);
            yield return new WaitForSeconds(1f/tmp.waveRate);
        }
        waveCount++;
    }
    void SpawnEnemie(GameObject enemy)
    {
        Instantiate(enemy,spawnPoint.position,spawnPoint.rotation);
        enemiesAlive++;
    }
}
