using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.PunBasics;

public class PauseMenu : MonoBehaviour
{
   
    public GameObject ui;
    public GameObject moneyUI;
    public GameObject liveUI;
    public GameObject waveTimerUI;
    public GameObject shopUI;
    public GameObject gameOverUI;
    public GameObject pauseUI;

    private GameManager gameManager;
    private WaveSpawner waveSpawner;

    bool gamePause = false;
    public string menuScene = "Menu";

    bool isEscapePressed = false; //bool to check if escape key is pressed

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            isEscapePressed = true;
            Toggle();
        }
        else if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P))
        {
            isEscapePressed = false;
        }
    }

    public void Toggle()
    {
        if (gamePause && !isEscapePressed)
        {
            ContinueGame();
        }
        else
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        ui.SetActive(true);
        gamePause = true;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
   
        moneyUI.SetActive(true); 
        liveUI.SetActive(true); 
        waveTimerUI.SetActive(true);
        shopUI.SetActive(true);

        pauseUI.SetActive(false);
        gamePause = false;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        ui.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        ui.SetActive(false);
        ResetGameState();
        ResetUI();
        gameManager.ResetGameManager();
        waveSpawner.ResetWaveSpawner();
        LoadNewScene();
    }

    public void ResetGameState()
    {
        PlayerStats.money = PlayerStats.startMoney;
        PlayerStats.lives = PlayerStats.startLives;
        PlayerStats.rounds = 0;
    }

    public void ResetUI()
    {
        moneyUI.SetActive(true);
        liveUI.SetActive(true);
        waveTimerUI.SetActive(true);
        shopUI.SetActive(true);
        gameOverUI.SetActive(false);
        pauseUI.SetActive(false);
    }
    public void LoadNewScene()
    {
        SceneManager.LoadScene(menuScene);
    }

}
