using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
   
    public GameObject ui;
    public GameObject moneyUI;
    public GameObject liveUI;
    public GameObject waveTimerUI;
    public GameObject shopUI;
    public GameObject gameOverUI;
    public GameObject pauseUI;

    bool gamePause = false;
    public string menuScene = "Menu";

    bool isEscapePressed = false; //bool to check if escape key is pressed


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
        SceneManager.LoadScene(menuScene);
    }

}
