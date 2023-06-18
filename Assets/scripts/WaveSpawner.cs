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

    private bool isWinGame = false;

    [SerializeField]
    private GameObject winGameUI;

    void Start()
    {
        winGameUI.SetActive(false);
    }

    void Update()
    {
        if (isWinGame)
        {
            ShowWinGameMenu();
            return;
        }

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
        if (WaveCount >= waveT.Length)
        {
            // Reached the end of waveT array, win game
            isWinGame = true;
            yield break;
        }

        PlayerStats.rounds++;

        Waves tmp = waveT[WaveCount];
        for (int i = 0; i < tmp.waveCount; i++)
        {
            SpawnEnemie(tmp.gameEnemie);
            yield return new WaitForSeconds(1f / tmp.waveRate);
        }
        WaveCount++;
    }

    void SpawnEnemie(GameObject gameEnemie)
    {
        Instantiate(gameEnemie, SpawnPoint.position, SpawnPoint.rotation);
        enemiesAlive++;
    }

    void ShowWinGameMenu()
    {
        winGameUI.SetActive(true);
    }

    public void ResetWaveSpawner()
    {
        StopAllCoroutines();
        enemiesAlive = 0;
        WaveCount = 0;
        isWinGame = false;
    }
}