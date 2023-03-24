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

    private float coutdown = 5f;

    [SerializeField]
    private Text WaveTimer;

    private int wavecount = 0;
    // Update is called once per frame
    void Update()
    {
        if(enemiesAlive>0)
        {
            return;
        }
        if(coutdown>0)
        {
            coutdown -= Time.deltaTime;
            coutdown = Mathf.Clamp(coutdown, 0f, Mathf.Infinity);
            WaveTimer.text = string.Format("{0:00.00}", coutdown);
        }
        else
        {
            StartCoroutine (spawnWave());
            coutdown = timeBwaves;
        }
    }
    IEnumerator spawnWave()
    {
        
        playerstats.rounds++;

        Waves tmp = waveT[wavecount];
        for (int i = 0; i < tmp.count; i++)
        {
            SpawnEnemie(tmp.enemie);
            yield return new WaitForSeconds(1f/tmp.rate);
        }
        wavecount++;
    }
    void SpawnEnemie(GameObject enemy)
    {
        Instantiate(enemy,SpawnPoint.position,SpawnPoint.rotation);
        enemiesAlive++;
    }
}
